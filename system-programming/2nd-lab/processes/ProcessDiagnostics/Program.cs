using Microsoft.Extensions.Configuration;
using ProcessDiagnostics.Library;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ProcessDiagnostics
{

    public static class Program
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\config.json")).Build();
        private static readonly int? _warningNotificationDelay = _config.GetValue<int?>("warningNotificationDelay");
        private static readonly int? _notificationDelay = _config.GetValue<int?>("notificationDelay");
        private static readonly string? _limitsFilePath = _config.GetValue<string?>("processLimitPath");

        public static event EventHandler<ProcessEventInfo>? ProcessEvent;

        private static int WarningNotificationDelay
        {
            get
            {
                if (_warningNotificationDelay is null)
                    throw new InvalidOperationException($"Config file hasn't been initialized.");

                return _warningNotificationDelay.Value;
            }
        }
        private static int NotificationDelay
        {
            get
            {
                if (_notificationDelay is null)
                    throw new InvalidOperationException($"Config file hasn't been initialized.");

                return _notificationDelay.Value;
            }
        }
        private static string LimitsFilePath
        {
            get
            {
                if (_limitsFilePath is null)
                    throw new InvalidOperationException($"Config file hasn't been initialized.");

                return _limitsFilePath;
            }
        }

        static async Task<int> Main(string[] args)
        {
            string processName = args[0];
            if (processName is null)
            {
                Console.WriteLine("Please, type in the name of the process you wish to inspect after the program's name.");
                return 0;
            }

            XmlSerializer xmlSerializer = new(typeof(ProcessLimit));
            ProcessLimit? processLimit;
            using (FileStream processLimitFileStream = new(LimitsFilePath, FileMode.Open))
            {
                processLimit = (ProcessLimit?)xmlSerializer.Deserialize(processLimitFileStream);
                if (processLimit is null)
                    return -1;
            }

            double fraction = .85;
            ProcessLimit dangerousThreshold = new()
            {
                MemoryUsageLimit = (long)(processLimit.MemoryUsageLimit * fraction),
                ProcessorTimeLimit = (int)(processLimit.ProcessorTimeLimit * fraction),
                ThreadCountLimit = (int)Math.Floor(processLimit.ThreadCountLimit * fraction),
                HandleCountLimit = (int)Math.Floor(processLimit.HandleCountLimit * fraction)
            };

            ProcessEventListener listener = new();
            ProcessEvent += listener.Log;
            using Process process = Process.Start(processName);

            ProcessMessageBuilder message = new(process);
            ProcessEvent.Invoke(null, new ProcessEventInfo(process) { Type = 0, Message = "Started" });
            Task[] warningTasks = new Task[4];
            for (int i = 0; i < warningTasks.Length; i++)
                warningTasks[i] = Task.CompletedTask;

            using CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Task logging = LogState(process, message, cancellationToken);

            while (!process.HasExited)
            {
                if (process.WorkingSet64 >= processLimit.MemoryUsageLimit)
                {
                    message.MemorySeverity = Severity.Error;
                    LogError(process, message);
                    process.Kill();
                    break;
                }
                else if (process.WorkingSet64 >= dangerousThreshold.MemoryUsageLimit)
                {
                    if (warningTasks[0].IsCompleted)
                    {
                        message.MemorySeverity = Severity.Warning;
                        warningTasks[0] = Task.Run(() => LogWarning(process, message));
                    }
                }
                else
                    message.MemorySeverity = Severity.None;


                if (process.TotalProcessorTime.Milliseconds >= processLimit.ProcessorTimeLimit)
                {
                    message.ProcessorTimeSeverity = Severity.Error;
                    LogError(process, message);
                    process.Kill();
                    break;
                }
                else if (process.TotalProcessorTime.Milliseconds >= dangerousThreshold.ProcessorTimeLimit)
                {
                    if (warningTasks[1].IsCompleted)
                    {
                        message.ProcessorTimeSeverity = Severity.Warning;
                        warningTasks[1] = Task.Run(() => LogWarning(process, message));
                    }
                }
                else
                    message.ProcessorTimeSeverity = Severity.None;


                if (process.Threads.Count >= processLimit.ThreadCountLimit)
                {
                    message.ThreadCountSeverity = Severity.Error;
                    LogError(process, message);
                    process.Kill();
                    break;
                }
                else if (process.Threads.Count >= dangerousThreshold.ThreadCountLimit)
                {
                    if (warningTasks[2].IsCompleted)
                    {
                        message.ThreadCountSeverity = Severity.Warning;
                        warningTasks[2] = Task.Run(() => LogWarning(process, message));
                    }
                }
                else
                    message.ThreadCountSeverity = Severity.None;

                if (process.HandleCount >= processLimit.HandleCountLimit)
                {
                    message.HandleCountSeverity = Severity.Error;
                    LogError(process, message);
                    process.Kill();
                    break;
                }
                else if (process.HandleCount >= dangerousThreshold.HandleCountLimit)
                {
                    if (warningTasks[3].IsCompleted)
                    {
                        message.HandleCountSeverity = Severity.Warning;
                        warningTasks[3] = Task.Run(() => LogWarning(process, message));
                    }
                }
                else
                    message.HandleCountSeverity = Severity.None;
            }

            cancellationTokenSource.Cancel();
            await logging;
            return 0;
        }

        /// <summary>
        /// Logs a notification message every specified(in the config file) seconds. Runs along with the state loop.
        /// </summary>
        /// <param name="process">Process we wish to track.</param>
        /// <param name="message">Message to be displayed in the log.</param>
        /// <param name="token">Cancellation token in case we'd like to stop.</param>
        /// <returns>Awaitable task representing the background work.</returns>
        private static async Task LogState(Process process, ProcessMessageBuilder message, CancellationToken token)
        {
            while (true)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(NotificationDelay), token).ContinueWith((_) =>
                    {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();

                        ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                        {
                            Type = ProcessEventType.Notification,
                            Message = message.ToString()
                        });
                    }, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Logs a warning message. It's made asynchronous because we'd like to log the message in specific time
        /// intervals once we've hit the warning state, while also keep the state loop checking the process for errors.
        /// </summary>
        /// <param name="process">Process we're currently tracking.</param>
        /// <param name="message">Warning message to be displayed in the log.</param>
        /// <returns>Awaitable task representing the background work.</returns>
        private static async Task LogWarning(Process process, ProcessMessageBuilder message)
        {
            ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
            {
                Type = ProcessEventType.Warning,
                Message = message.ToString()
            });
            await Task.Delay(TimeSpan.FromSeconds(WarningNotificationDelay));
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="process">Process we're currently tracking.</param>
        /// <param name="message">Error message to be displayed in the log.</param>
        private static void LogError(Process process, ProcessMessageBuilder message)
        {
            ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
            {
                Type = ProcessEventType.Error,
                Message = message.ToString()
            });
        }
    }
}
















/* 
 using Microsoft.Extensions.Configuration;
using ProcessDiagnostics.Library;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Timers;

namespace ProcessDiagnostics
{

    public static class Program
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\config.json")).Build();
        public static event EventHandler<ProcessEventInfo>? ProcessEvent;

        static async Task<int> Main(string[] args)
        {
            string processName = args[0];
            if (processName is null)
            {
                Console.WriteLine("Please, type in the name of the process you wish to inspect after the program's name.");
                return 0;
            }

            string? processLimitFilePath = _config["processLimitPath"];
            if (processLimitFilePath is null)
                return -1;

            XmlSerializer xmlSerializer = new(typeof(ProcessLimit));
            ProcessLimit? processLimit;
            using (FileStream processLimitFileStream = new(processLimitFilePath, FileMode.Open))
            {
                processLimit = (ProcessLimit?)xmlSerializer.Deserialize(processLimitFileStream);
                if (processLimit is null)
                    return -1;
            }

            double fraction = .85;
            ProcessLimit dangerousThreshold = new()
            {
                MemoryUsageLimit = (long)(processLimit.MemoryUsageLimit * fraction),
                ProcessorTimeLimit = (int)(processLimit.ProcessorTimeLimit * fraction),
                ThreadCountLimit = (int)Math.Floor(processLimit.ThreadCountLimit * fraction),
                HandleCountLimit = (int)Math.Floor(processLimit.HandleCountLimit * fraction)
            };
            ProcessEventListener listener = new();
            ProcessEvent += listener.Log;
            using Process process = Process.Start(processName);
            ProcessMessageBuilder message = new(process);
            ProcessEvent.Invoke(null, new ProcessEventInfo(process) { Type = 0, Message = "Started" });

            using CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Task logging = LogState(process, message, cancellationToken);
            System.Timers.Timer timer = new(1000) { AutoReset = true };
            timer.Start();
            while (!process.HasExited)
            {
                if (process.WorkingSet64 >= processLimit.MemoryUsageLimit)
                {
                    message.MemorySeverity = Severity.Error;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Error,
                        Message = message.ToString()
                    });
                    process.Kill();
                    break;
                }
                else if (process.WorkingSet64 >= dangerousThreshold.MemoryUsageLimit)
                {
                    message.MemorySeverity = Severity.Warning;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Warning,
                        Message = message.ToString()
                    });
                }
                else
                    message.MemorySeverity = Severity.None;


                if (process.TotalProcessorTime.Milliseconds >= processLimit.ProcessorTimeLimit)
                {
                    message.ProcessorTimeSeverity = Severity.Error;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Error,
                        Message = message.ToString()
                    });
                    process.Kill();
                    break;
                }
                else if (process.TotalProcessorTime.Milliseconds >= dangerousThreshold.ProcessorTimeLimit)
                {
                    message.ProcessorTimeSeverity = Severity.Warning;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Warning,
                        Message = message.ToString()
                    });
                }
                else
                    message.ProcessorTimeSeverity = Severity.None;


                if (process.Threads.Count >= processLimit.ThreadCountLimit)
                {
                    message.ThreadCountSeverity = Severity.Error;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Error,
                        Message = message.ToString()
                    });
                    process.Kill();
                    break;
                }
                else if (process.Threads.Count >= dangerousThreshold.ThreadCountLimit)
                {
                    message.ThreadCountSeverity = Severity.Warning;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Warning,
                        Message = message.ToString()
                    });
                }
                else
                    message.ThreadCountSeverity = Severity.None;

                if (process.HandleCount >= processLimit.HandleCountLimit)
                {
                    message.HandleCountSeverity = Severity.Error;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Error,
                        Message = message.ToString()
                    });
                    process.Kill();
                    break;
                }
                else if (process.HandleCount >= dangerousThreshold.HandleCountLimit)
                {
                    message.HandleCountSeverity = Severity.Warning;

                    ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                    {
                        Type = ProcessEventType.Warning,
                        Message = message.ToString()
                    });
                }
                else
                    message.HandleCountSeverity = Severity.None;
            }

            cancellationTokenSource.Cancel();
            await logging;
            return 0;
        }

        private static async Task LogState(Process process, ProcessMessageBuilder message, CancellationToken token)
        {
            while (true)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(2), token).ContinueWith((_) =>
                    {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();

                        ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
                        {
                            Type = ProcessEventType.Notification,
                            Message = message.ToString()
                        });
                    }, token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}*/
using Microsoft.Extensions.Configuration;
using ProcessDiagnostics.Library;
using System.Diagnostics;
using System.Xml.Serialization;

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
            ProcessLimit? processLimits;
            using (FileStream processLimitFileStream = new(processLimitFilePath, FileMode.Open))
            {
                processLimits = (ProcessLimit?)xmlSerializer.Deserialize(processLimitFileStream);
                if (processLimits is null)
                    return -1;
            }

            double fraction = .85;
            ProcessLimit dangerousThreshold = new()
            {
                MemoryUsageLimit = (long)(processLimits.MemoryUsageLimit * fraction),
                ProcessorTimeLimit = (int)(processLimits.ProcessorTimeLimit * fraction),
                ThreadCountLimit = (int)Math.Floor(processLimits.ThreadCountLimit * fraction),
                HandleCountLimit = (int)Math.Floor(processLimits.HandleCountLimit * fraction)
            };
            ProcessEventListener listener = new();
            ProcessEvent += listener.Log;
            using Process process = Process.Start(processName);
            ProcessMessageBuilder message = new(process);
            ProcessEvent.Invoke(null, new ProcessEventInfo(process) { Type = 0, Message = "Started" });

            using CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Task logging = LogState(process, message, cancellationToken);
            while (!process.HasExited)
            {
                if (process.WorkingSet64 >= processLimits.MemoryUsageLimit)
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


                if (process.TotalProcessorTime.Milliseconds >= processLimits.ProcessorTimeLimit)
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


                if (process.Threads.Count >= processLimits.ThreadCountLimit)
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

                if (process.HandleCount >= processLimits.HandleCountLimit)
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
}
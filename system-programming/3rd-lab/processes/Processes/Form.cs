using Microsoft.Extensions.Configuration;
using ProcessTracker.Library;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Processes
{
    public partial class ProcessesTracker : Form
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\config.json")).Build();
        private readonly ProcessLimit? _processLimit;
        private readonly ProcessLimit _dangerousThreshold;
        private static readonly string? _limitsFilePath = _config["processLimitPath"];
        private static readonly int? _processorCountLimit = _config.GetValue<int>("processCountLimit");
        private static readonly CancellationTokenSource cancellationTokenSource = new();
        private readonly List<Process> _activeProcesses = new();
        private readonly LinkedList<Task> _tasks = new();

        private static string LimitsFilePath
        {
            get
            {
                if (_limitsFilePath is null)
                    throw new InvalidOperationException($"Config file hasn't been initialized.");

                return _limitsFilePath;
            }
        }

        private static int ProcessorCountLimit
        {
            get
            {
                if (_processorCountLimit is null)
                    throw new InvalidOperationException($"Config file hasn't been initialized.");

                return _processorCountLimit.Value;
            }
        }

        public static event EventHandler<ProcessEventInfo>? ProcessEvent;

        public ProcessesTracker()
        {
            InitializeComponent();
            XmlSerializer xmlSerializer = new(typeof(ProcessLimit));
            using (FileStream processLimitFileStream = new(LimitsFilePath, FileMode.Open))
            {
                _processLimit = (ProcessLimit?)xmlSerializer.Deserialize(processLimitFileStream);
                if (_processLimit is null)
                    throw new InvalidOperationException();
            }

            double fraction = .85;
            _dangerousThreshold = new()
            {
                MemoryUsageLimit = (long)(_processLimit.MemoryUsageLimit * fraction),
                ProcessorTimeLimit = (int)(_processLimit.ProcessorTimeLimit * fraction),
                ThreadCountLimit = (int)Math.Floor(_processLimit.ThreadCountLimit * fraction),
                HandleCountLimit = (int)Math.Floor(_processLimit.HandleCountLimit * fraction)
            };
            ProcessEventListener listener = new();
            ProcessEvent += listener.Log;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridView.Columns[nameof(terminateProcessButton)].Index)
                return;

            Process process = Process.GetProcessById((int)dataGridView.Rows[e.RowIndex].Cells["ProcessId"].Value);
            process.Kill();
        }

        private void TerminateAllButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                lock (_activeProcesses)
                {
                    // Waiting for all condition loops to end in order to safely close every process.
                    cancellationTokenSource.Cancel();
                    Task.WaitAll(_tasks.ToArray());

                    foreach (Process process in _activeProcesses)
                        process.Kill();
                    _activeProcesses.Clear();
                }
            });
        }

        private void AddProcessButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            string[] fileNames = openFileDialog.FileNames;
            int totalProcesses = _activeProcesses.Count + fileNames.Length;
            if (totalProcesses > ProcessorCountLimit)
                return;

            foreach (string fileName in fileNames)
            {
                Process process = Process.Start(fileName);
                string processName = process.ProcessName;
                int processId = process.Id;
                process.EnableRaisingEvents = true;
                // Do not lock on the main UI thread. It leads to unexpected deadlocks.
                Task.Run(() =>
                {
                    lock (_activeProcesses) { _activeProcesses.Add(process); }
                });
                CancellationToken cancellation = cancellationTokenSource.Token;
                _tasks.AddLast(Task.Run(() => LogState(process, cancellation), cancellation));

                int index = dataGridView.Rows.Add(process.ProcessName, process.WorkingSet64, process.TotalProcessorTime, process.Threads.Count, process.HandleCount, process.Id);
                DataGridViewRow createdRow = dataGridView.Rows[index];
                process.Exited += (_, _) =>
                {
                    Task.Run(() =>
                    {
                        if (process.EnableRaisingEvents)
                            ProcessEvent?.Invoke(null, new ProcessEventInfo() 
                            {   Type = 0,
                                Name = $"{processName}{processId}",
                                Message = "Finished",
                                Id = processId,
                                Time = DateTime.Now 
                            });

                        lock (_activeProcesses)
                        {
                            _activeProcesses.Remove(process);
                            dataGridView.Invoke(() => dataGridView.Rows.Remove(createdRow));
                        }
                    });
                };
            }

            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (_activeProcesses.Count != 0 && !backgroundWorker.CancellationPending)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                lock (_activeProcesses)
                {
                    // DataGridView.Invoke() method is a synchronous method that is called on the thread
                    // which owns the DataGridView instance (or any WinForm controls for that matter).
                    foreach (Process process in _activeProcesses)
                        dataGridView.Invoke(() => { UpdateGridView(); });
                }
            }
        }

        private void UpdateGridView()
        {
            for (int i = 0; i < _activeProcesses.Count; i++)
            {
                Process process = _activeProcesses[i];
                if (!process.HasExited)
                {
                    dataGridView.Rows[i].SetValues(process.ProcessName, process.WorkingSet64, process.TotalProcessorTime, process.Threads.Count, process.HandleCount, process.Id);
                }
            }
        }

        private void LogState(Process process, CancellationToken cancellation)
        {
            ProcessEvent?.Invoke(null, new ProcessEventInfo(process) { Type = 0, Message = "Started" });
            int ticks = 0;
            ProcessMessageBuilder message = new(process);
            do
            {
                process.Refresh();
                Severity eventSeverity = Severity.Notification;
                // Accessing an exited process's properties throws an InvalidOperationException. We should be
                // ready for this exception in case user closes an application we're currently checking for
                // conditions.
                try
                {
                    if (process.WorkingSet64 >= _processLimit!.MemoryUsageLimit)
                    {
                        message.MemorySeverity = Severity.Error;
                        LogError(process, message);
                        process.Kill();
                        break;
                    }
                    else if (process.WorkingSet64 >= _dangerousThreshold.MemoryUsageLimit)
                    {
                        message.MemorySeverity = Severity.Warning;
                        eventSeverity = Severity.Warning;
                    }
                    else
                        message.MemorySeverity = Severity.Notification;


                    if (process.TotalProcessorTime.Milliseconds >= _processLimit.ProcessorTimeLimit)
                    {
                        message.ProcessorTimeSeverity = Severity.Error;
                        LogError(process, message);
                        process.Kill();
                        break;
                    }
                    else if (process.TotalProcessorTime.Milliseconds >= _dangerousThreshold.ProcessorTimeLimit)
                    {
                        message.ProcessorTimeSeverity = Severity.Warning;
                        eventSeverity = Severity.Warning;
                    }
                    else
                        message.ProcessorTimeSeverity = Severity.Notification;


                    if (process.Threads.Count >= _processLimit.ThreadCountLimit)
                    {
                        message.ThreadCountSeverity = Severity.Error;
                        LogError(process, message);
                        process.Kill();
                        break;
                    }
                    else if (process.Threads.Count >= _dangerousThreshold.ThreadCountLimit)
                    {
                        message.ThreadCountSeverity = Severity.Warning;
                        eventSeverity = Severity.Warning;
                    }
                    else
                        message.ThreadCountSeverity = Severity.Notification;

                    if (process.HandleCount >= _processLimit.HandleCountLimit)
                    {
                        message.HandleCountSeverity = Severity.Error;
                        LogError(process, message);
                        process.Kill();
                        break;
                    }
                    else if (process.HandleCount >= _dangerousThreshold.HandleCountLimit)
                    {
                        message.HandleCountSeverity = Severity.Warning;
                        eventSeverity = Severity.Warning;
                    }
                    else
                        message.HandleCountSeverity = Severity.Notification;

                    if (ticks == 10)
                    {
                        ProcessEvent?.Invoke(null, new ProcessEventInfo(process) { Type = eventSeverity, Message = message.ToString() });
                    }
                }
                // Just break out of the conditions loop. The final log is handled by the process's Exited event.
                catch (InvalidOperationException)
                {
                    break;
                }

                ticks = (ticks + 1) % 11;
            }
            while (!cancellation.IsCancellationRequested && !process.WaitForExit(100));
        }

        private static void LogError(Process process, ProcessMessageBuilder message)
        {
            ProcessEvent?.Invoke(null, new ProcessEventInfo(process)
            {
                Type = Severity.Error,
                Message = message.ToString()
            });

            process.EnableRaisingEvents = false;
        }
    }
}
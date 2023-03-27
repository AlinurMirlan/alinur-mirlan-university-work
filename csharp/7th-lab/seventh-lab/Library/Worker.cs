using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 1st task
    public class Worker
    {
        private static readonly Random random = new();
        private readonly Logger logger;
        private readonly Mutex mutex;

        public Worker(string mutexName, string logFilePath)
        {
            this.mutex = new Mutex(false, mutexName, out _);
            this.logger = new FileLogger(logFilePath);
        }

        public Task Run(CancellationToken cancelToken) => Task.Run(() => Work(mutex, cancelToken), cancelToken);

        private void Work(Mutex mutex, CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                Task[] tasks =
                {
                    Task.Run(() => Workload(mutex), cancelToken),
                    Task.Run(() => Workload(mutex), cancelToken),
                };

                Task.WaitAll(tasks, cancelToken);
            }
        }

        private void Workload(Mutex mutex)
        {
            mutex.WaitOne();

            try
            {
                // Simulate work
                Thread.Sleep(random.Next(10) + 990);
                // Write message to a file
                logger.Log($"process: {Process.GetCurrentProcess().ProcessName}, thread: {Thread.CurrentThread.Name}, output: {random.NextDouble() * 1000}");
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}

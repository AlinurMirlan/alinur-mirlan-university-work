using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhLab
{
    // 3rd task
    public class ValueFormatter
    {
        private static readonly Random random = new();
        private double value;

        public void RunFormatters(params string[] formats)
        {
            ManualResetEvent manualResetEvent = new(false);
            Task[] preprocessingTasks = new Task[formats.Length];
            for (int i = 0;i < formats.Length; i++)
            {
                preprocessingTasks[i] = StartupAndProcessValueTask($"Formatter #{i + 1}", formats[i], manualResetEvent);
            }

            // At this point the current thread will halt for 3 seconds, and then signal for waiting threads
            // to proceed with their formatting duties once the value to work on has been delivered to them.
            value = FetchValue();
            manualResetEvent.Set();
            Task.WaitAll(preprocessingTasks);
        }

        // Let's just imagine this method connects to some remote database that performs a heavy computational work
        // to get the data we want to format.
        private static double FetchValue()
        {
            // Simulating a 3 seconds delay.
            Thread.Sleep(TimeSpan.FromSeconds(4));
            return 100 + random.Next(900) * random.NextDouble();
        }

        private Task StartupAndProcessValueTask(string processName, string format, ManualResetEvent manualResetEvent)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Starting up {processName}.");
                // Simulating some work that takes from 2 up to 2.5 seconds.
                Thread.Sleep(2000 + random.Next(500));
                // Since we're expected to receive the value to work on a little later, we ought to wait for it
                // via ManualResentEvent.
                Console.WriteLine($"{processName} has finished starting up. Waiting for the value to be fetched.");
                manualResetEvent.WaitOne();
                Console.WriteLine($"{processName} has formatted {value} to {value.ToString(format)}");
            });
        }
    }
}

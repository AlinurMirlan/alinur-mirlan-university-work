using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes
{
    public static class ExtensionMethods
    {
        public static int RamUsage(this Process process)
        {
            int ramUsage = 0;
            if (!process.HasExited)
            {
                PerformanceCounter perfCounter = new();
                perfCounter.CategoryName = "Process";
                perfCounter.CounterName = "Working Set - Private";
                perfCounter.InstanceName = process.ProcessName;
                ramUsage = Convert.ToInt32(perfCounter.NextValue()) / (int)(1024);
                perfCounter.Close();
                perfCounter.Dispose();
            }
            return ramUsage;
        }
    }
}

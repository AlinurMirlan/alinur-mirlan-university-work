using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Logger
    {
        public string FilePath { get; set; }

        protected Logger(string filePath)
        {
            FilePath = filePath;
        }

        public abstract void Log(string message);
    }
}

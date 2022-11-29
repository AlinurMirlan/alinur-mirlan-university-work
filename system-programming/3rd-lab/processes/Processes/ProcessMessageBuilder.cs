using ProcessTracker.Library;
using System.Diagnostics;
using System.Text;

namespace Processes
{
    /// <summary>
    /// Represents the severity of the process statistics. 
    /// </summary>

    public class ProcessMessageBuilder
    {
        private readonly Process _process;

        public ProcessMessageBuilder(Process process)
        {
            this._process = process;
        }

        public Severity MemorySeverity { get; set; }

        public Severity ProcessorTimeSeverity { get; set; }

        public Severity ThreadCountSeverity { get; set; }

        public Severity HandleCountSeverity { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append($"{IndentSeverityText(this.MemorySeverity)}Memory: {_process.RamUsage()}; ");
            builder.Append($"{IndentSeverityText(this.ProcessorTimeSeverity)}Processor time:{_process.TotalProcessorTime.Milliseconds}; ");
            builder.Append($"{IndentSeverityText(this.ThreadCountSeverity)}Thread count: {_process.Threads.Count}; ");
            builder.Append($"{IndentSeverityText(this.HandleCountSeverity)}Handle count: {_process.HandleCount}; ");

            return builder.ToString();
        }

        private static string IndentSeverityText(Severity severity) => severity == 0 ? string.Empty : $"{severity} ";
    }
}

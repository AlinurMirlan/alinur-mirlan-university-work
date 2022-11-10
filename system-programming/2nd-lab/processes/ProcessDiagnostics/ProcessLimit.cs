using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;

namespace ProcessDiagnostics
{
    [XmlRoot("runinfo")]
    public class ProcessLimit
    {
        [XmlElement("memory_limit")]
        public long MemoryUsageLimit { get; set; }

        [XmlElement("processor_time_limit")]
        public int ProcessorTimeLimit { get; set; }

        [XmlElement("thread_count_limit")]
        public int ThreadCountLimit { get; set; }

        [XmlElement("handle_count_limit")]
        public int HandleCountLimit { get; set; }
    }
}

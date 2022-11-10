using System.Diagnostics;
using System.Xml.Serialization;

namespace ProcessDiagnostics.Library
{
    public enum ProcessEventType
    {
        Notification, Warning, Error
    }

    [XmlRoot("event")]
    public class ProcessEventInfo
    {
        public ProcessEventInfo(Process process)
        {
            this.ProcessId = process.Id;
            this.Process = process.ProcessName;
            this.Time = DateTime.Now;
        }

        public ProcessEventInfo() { }

        [XmlElement("type")]
        public ProcessEventType Type { get; set; }

        [XmlElement("process_id")]
        public int ProcessId { get; set; }

        [XmlElement("process")]
        public string? Process { get; set; }

        [XmlElement("message")]
        public string? Message { get; set; }

        [XmlElement("time")]
        public DateTime Time { get; set; }
    }
}
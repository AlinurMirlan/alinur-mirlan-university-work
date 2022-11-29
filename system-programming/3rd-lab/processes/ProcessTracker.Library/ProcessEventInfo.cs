using System.Diagnostics;
using System.Xml.Serialization;

namespace ProcessTracker.Library
{
    public enum Severity
    {
        Notification, Warning, Error
    }

    [XmlRoot("event")]
    public class ProcessEventInfo
    {
        public ProcessEventInfo(Process process)
        {
            this.Id = process.Id;
            this.Name = $"{process.ProcessName}{process.Id}";
            this.Time = DateTime.Now;
        }

        public ProcessEventInfo() { }

        [XmlElement("type")]
        public Severity Type { get; set; }

        [XmlElement("process_id")]
        public int Id { get; set; }

        [XmlElement("process")]
        public string? Name { get; set; }

        [XmlElement("message")]
        public string? Message { get; set; }

        [XmlElement("time")]
        public DateTime Time { get; set; }
    }
}
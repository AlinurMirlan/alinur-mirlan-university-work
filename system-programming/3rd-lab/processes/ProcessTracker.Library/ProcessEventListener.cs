using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace ProcessTracker.Library
{
    public class ProcessEventListener
    {
        private static readonly XmlWriterSettings _settings = new () { Indent = true, OmitXmlDeclaration = true };
        private static readonly XmlSerializerNamespaces _xmlns = new (new[] { XmlQualifiedName.Empty });
        private static readonly object _lock = new();
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\ProcessTracker.Library\config.json")).Build();
        private static readonly string _logFilesLocation = _config["logFilesLocation"] ?? throw new InvalidOperationException();

        public ProcessEventListener()
        {
            DirectoryInfo directoryInfo = new(_logFilesLocation);
            directoryInfo.Empty();
        }

        public void Log(object? _, ProcessEventInfo? processInfo)
        {
            lock (_lock)
            {
                string logFilePath = Path.Combine(_logFilesLocation, $"{processInfo?.Name}.xml");
                using StreamWriter streamWriter = new(logFilePath, true);
                using XmlWriter writer = XmlWriter.Create(streamWriter, _settings);
                XmlSerializer xmlSerializer = new(typeof(ProcessEventInfo));
                xmlSerializer.Serialize(writer, processInfo, _xmlns);
                streamWriter.Write(Environment.NewLine);
            }
        }
    }
}

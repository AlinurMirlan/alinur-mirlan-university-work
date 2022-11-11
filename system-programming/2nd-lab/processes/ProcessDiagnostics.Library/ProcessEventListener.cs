using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace ProcessDiagnostics.Library
{
    public class ProcessEventListener
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\ProcessDiagnostics.Library\config.json")).Build();
        private static readonly string? _logFilePath = _config["processLogPath"];
        private readonly XmlWriterSettings _settings;
        private readonly XmlSerializerNamespaces _xmlns;
        private readonly object _lock = new();
        private static string LogFilePath
        {
            get
            {
                if (_logFilePath is null)
                    throw new InvalidOperationException($"Config file hasn't been initialized.");

                return _logFilePath;
            }
        }

        public ProcessEventListener()
        {
            File.Create(LogFilePath).Dispose();
            _settings = new() { Indent = true, OmitXmlDeclaration = true };
            _xmlns = new(new[] { XmlQualifiedName.Empty });
        }

        public void Log(object? _, ProcessEventInfo? processInfo)
        {
            lock (_lock)
            {
                using StreamWriter streamWriter = new(LogFilePath, true);
                using XmlWriter writer = XmlWriter.Create(streamWriter, _settings);
                XmlSerializer xmlSerializer = new(typeof(ProcessEventInfo));
                xmlSerializer.Serialize(writer, processInfo, _xmlns);
                streamWriter.Write(Environment.NewLine);
            }
        }
    }
}

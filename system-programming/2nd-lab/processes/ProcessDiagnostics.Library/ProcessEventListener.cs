using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace ProcessDiagnostics.Library
{
    public class ProcessEventListener
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\ProcessDiagnostics.Library\config.json")).Build();
        private static readonly string? _logFilePath = _config["processLogPath"];

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
        }

        public void Log(object? _, ProcessEventInfo? processInfo)
        {
            XmlWriterSettings settings = new() { Indent = true, OmitXmlDeclaration = true };
            XmlSerializerNamespaces xmlns = new(new[] { XmlQualifiedName.Empty });
            lock (settings)
            {
                using StreamWriter streamWriter = new(LogFilePath, true);
                using XmlWriter writer = XmlWriter.Create(streamWriter, settings);
                XmlSerializer xmlSerializer = new(typeof(ProcessEventInfo));
                xmlSerializer.Serialize(writer, processInfo, xmlns);
                streamWriter.Write(Environment.NewLine);
            }
        }
    }
}

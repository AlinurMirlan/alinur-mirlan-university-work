namespace Library
{
    public class FileLogger : Logger
    {
        public FileLogger(string filePath): base(filePath) { }

        public override void Log(string message)
        {
            StreamWriter writer = new(new FileStream(FilePath, FileMode.Append));
            writer.WriteLine(message);
            writer.Dispose();
        }
    }
}
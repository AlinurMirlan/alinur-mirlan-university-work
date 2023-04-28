using Library;
using Microsoft.Extensions.Configuration;

#region Configuration initialization
string configFilePath = Path.GetFullPath("../../../../config.json", AppDomain.CurrentDomain.BaseDirectory);
IConfiguration config = new ConfigurationBuilder().AddJsonFile(configFilePath).Build();
#endregion

// First task
string logFilePath = config["logFilePath"] ?? throw new InvalidDataException();
string mutexName = config["MutexName"] ?? throw new InvalidDataException();

CancellationTokenSource tokenSource = new();
CancellationToken cancelToken = tokenSource.Token;
Task loggingTask = new Worker(mutexName, logFilePath).RunAsync(cancelToken);
Console.WriteLine("Press 'Enter' to shutdown.");
Console.ReadLine();
tokenSource.Cancel();
try
{
    loggingTask.Wait();
}
catch (AggregateException aggregateException)
{
    foreach (Exception exception in aggregateException.InnerExceptions)
    {
        if (exception is TaskCanceledException)
            Console.WriteLine("Terminated.");
        else throw;
    }
}

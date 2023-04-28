using SeventhLab;
using SeventhLab.Infrastructure;
using SeventhLab.ShoeMakers;


//StackHeap();
/*ValueFormatter valueFormatter = new();
valueFormatter.RunFormatters("000.000", "###", "%00.00");*/
ShoeMaking();
//Demonstrate();
#region Semaphore usage methods

// 2nd task
void StackHeap()
{
    int heap = 0;
    SemaphoreSlim semaphore = new(3, 3);

    // Hot tasks
    Task[] heapStackers = new Task[6];
    for (int i = 0; i < heapStackers.Length; i++)
        heapStackers[i] = Work($"Worker {i + 1}");

    Task.WaitAll(heapStackers);

    Task Work(string name)
    {
        return Task.Run(() =>
        {
            Thread.CurrentThread.Name = name;
            Console.WriteLine($"{Thread.CurrentThread.Name} waits till the semaphore is up.");
            semaphore.Wait();
            Console.WriteLine($"{Thread.CurrentThread.Name} took up one of the locks.");
            Thread.Sleep(995 + Generator.Random.Next(5));
            Console.WriteLine($"{Thread.CurrentThread.Name} released one of the locks and updated the heap up to {Interlocked.Add(ref heap, 100)}.");
            semaphore.Release();
        });
    }
}


#endregion

#region AutoResentEvent usage in 'producer consumer' pattern

void ShoeMaking()
{
    ShoeShop shoeShop = new(3, new NikeShoes(), new NewBalanceShoes());
    shoeShop.Open();
    Thread.Sleep(100000);
}

#endregion

#region async method consumption
async void Demonstrate()
{
    // 1st method
    Task<int> fetchValueTask = FetchValueAsync();
    int value = await fetchValueTask;
    Console.WriteLine($"fetched #1 {value}");

    fetchValueTask = FetchValueAsync();
    // 2nd method
    while (!fetchValueTask.IsCompleted)
    {
        Thread.Sleep(100);
        Console.Write(".");
    }
    value = fetchValueTask.Result;
    Console.WriteLine($"fetched #2 {value}");

    // 4th method
    Task<int> continuation = fetchValueTask.ContinueWith((antecedent) => antecedent.Result + 1);
    int processedValue = await continuation;
    Console.WriteLine($"fetched #2 and processsed it to be {processedValue}");

    // 3rd method
    await OnCompleteTask(() => Console.WriteLine("process finished"));
}

async Task<int> FetchValueAsync()
{
    await Task.Delay(TimeSpan.FromSeconds(2));
    return 1;
}

async Task OnCompleteTask(Action callbackFunction)
{
    await Task.Delay(TimeSpan.FromSeconds(2));
    callbackFunction();
}

#endregion
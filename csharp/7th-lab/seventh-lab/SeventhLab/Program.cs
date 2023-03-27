using SeventhLab;
using SeventhLab.Infrastructure;
using SeventhLab.ShoeMakers;


/*StackHeap();
ValueFormatter valueFormatter = new();
valueFormatter.RunFormatters("000.000", "###", "%00.00");*/
ShoeMaking();


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

void ShoeMaking()
{
    ShoeShop shoeShop = new(1, new NikeShoes(), new NewBalanceShoes(), new LiningShoes());
    shoeShop.Open();
    Thread.Sleep(100000);
}

async void Demonstrate()
{
    // 1st method
    Task<int> fetchValueTask = FetchValueAsync();
    int value = await fetchValueTask;

    // 2nd method
    while (!fetchValueTask.IsCompleted)
    {
        Thread.Sleep(100);
    }

    // 3rd method
    value = fetchValueTask.Result;
    Task<int> continuation = fetchValueTask.ContinueWith((antecedent) => antecedent.Result + 1);
    int newValue = await continuation;

    // 4th method
    await DoSomethingOnCompletedAsync(() => Console.WriteLine("Finished."));
}

async Task<int> FetchValueAsync()
{
    await Task.Delay(TimeSpan.FromSeconds(2));
    return 1;
}

async Task DoSomethingOnCompletedAsync(Action callback)
{
    await Task.Delay(TimeSpan.FromSeconds(2));
    callback();
}
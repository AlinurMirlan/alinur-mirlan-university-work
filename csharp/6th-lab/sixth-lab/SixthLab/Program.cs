RunSomeThreads();


#region private members
void DeadLock()
{
    object lockOne = new();
    object lockTwo = new();
    Thread threadOne = new(() =>
    {
        Thread.Sleep(TimeSpan.FromMilliseconds(100));
        Console.WriteLine($"{nameof(threadOne)} has taken up the {nameof(lockOne)} lock.");
        lock (lockOne)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine($"{nameof(threadOne)} is waiting for the {nameof(lockTwo)} lock.");
            lock (lockTwo)
            {
                Console.WriteLine("This code block will never run.");
            }
        }
    });
    Thread threadTwo = new(() =>
    {
        Thread.Sleep(TimeSpan.FromMilliseconds(110));
        Console.WriteLine($"{nameof(threadTwo)} has taken up the {nameof(lockTwo)} lock.");
        lock (lockTwo)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1.3));
            Console.WriteLine($"{nameof(threadTwo)} is waiting for the {nameof(lockOne)} lock.");
            lock (lockOne)
            {
                Console.WriteLine("This code block will never run.");
            }
        }
    });

    threadOne.Start();
    threadTwo.Start();
}
void RunSomeThreads()
{
    int threadsCount = 10;
    Thread[] threads = new Thread[threadsCount];
    for (int i = 0; i < threadsCount; i++)
    {
        threads[i] = new Thread(writeName);
    }

    foreach (Thread thread in threads)
    {
        thread.Start();
    }

    void writeName()
    {
        Console.WriteLine(Environment.CurrentManagedThreadId);
    }
}
#endregion
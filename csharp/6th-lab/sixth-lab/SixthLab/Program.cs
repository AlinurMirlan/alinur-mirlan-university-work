using SixthLab;
using SixthLab.TaskChaining;
using System.Collections.Concurrent;
using System.Reflection.Metadata;

ArrayManipulation.Process();

#region unsafe stack
/*while (true)
{
    RunUnsafeStack();
}*/
#endregion


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
    int threadsCount = 5;
    Thread[] threads = new Thread[threadsCount];
    for (int i = 0; i < threadsCount; i++)
    {
        threads[i] = new Thread(writeName) { Priority = (ThreadPriority)i };
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

void RunUnsafeStack()
{
    System.Collections.Generic.Stack<int> stack = new();
    for (int i = 0; i < 20; i++)
    {
        stack.Push(i);
    }

    Task[] tasks = new Task[10];
    for (int i = 0; i < tasks.Length; i++)
    {
        tasks[i] = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                if (stack.Count != 0)
                    stack.Pop();
            }
        });
    }

    Task.WaitAll(tasks);
    Console.WriteLine(stack.Count);
}

void RunSafeStack()
{
    SixthLab.Collections.Stack<int> stack = new();
    for (int i = 0; i < 20; i++)
    {
        stack.Push(i);
    }

    Task[] tasks = new Task[10];
    for (int i = 0; i < tasks.Length; i++)
    {
        tasks[i] = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                if (stack.Count != 0)
                    stack.TryPop(out int value);
            }
        });
    }

    Task.WaitAll(tasks);
    Console.WriteLine(stack.Count);
}
#endregion
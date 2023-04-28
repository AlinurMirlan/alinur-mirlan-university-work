using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SeventhLab.Infrastructure;
using SeventhLab.ShoeMakers;

namespace SeventhLab;

public class ShoeShop
{
    private readonly CancellationTokenSource cancellationTokenSource = new();
    private const int MaxQueueSize = 5;
    private readonly int clientelle;
    private readonly AutoResetEvent notOutOfStockEvent = new(false);
    private readonly AutoResetEvent notFullStockEvent = new(false);
    private readonly object @lock = new();
    /// <summary>
    /// Stack for checking in and out waiting deliveries.
    /// </summary>
    private readonly Stack<bool> waitingDeliveries = new();
    /// <summary>
    /// Stack for checking in and out waiting customers. 
    /// </summary>
    private readonly Stack<bool> waitingCustomers = new();

    public List<IShoeMaker> ShoeMakers { get; } = new();
    public Queue<string> ShoesQueue { get; } = new();

    public ShoeShop(int clientelle, params IShoeMaker[] shoeMakers)
    {
        this.clientelle = clientelle;
        ShoeMakers.AddRange(shoeMakers);
    }

    public void Open()
    {
        CancellationToken cancelToken = cancellationTokenSource.Token;
        foreach (IShoeMaker shoeMaker in ShoeMakers)
        {
            Task.Run(() => EnqueueShoes(shoeMaker, cancelToken), cancelToken);
        }

        for (int i = 0; i < clientelle; i++)
        {
            Task.Run(() => DequeueShoes(cancelToken), cancelToken);
        }
    }

    public void Close() => cancellationTokenSource.Cancel();

    /// <summary>
    /// Simulates delivering a shoe from a given shoe maker to the shop every now and then.
    /// </summary>
    /// <param name="shoeMaker">A shoe maker who is to deliver one of its shoes every once in a while</param>
    /// <param name="cancelToken">Cancellation token in case we close the shop.</param>
    private void EnqueueShoes(IShoeMaker shoeMaker, CancellationToken cancelToken)
    {
        while (!cancelToken.IsCancellationRequested)
        {
            string shoe = shoeMaker.DeliverShoe();
            try
            {
                Monitor.Enter(@lock);
                if (ShoesQueue.Count >= MaxQueueSize)
                {
                    waitingDeliveries.Push(true);
                    Console.WriteLine($"Depot is full. {shoeMaker.Brand} shoes are suspended.");
                    Monitor.Exit(@lock);
                    notFullStockEvent.WaitOne();
                    Monitor.Enter(@lock);
                }

                ShoesQueue.Enqueue(shoe);
                Console.WriteLine($"Shoe {shoe} has been delivered and stored, stocking shoes up to {ShoesQueue.Count}");
                if (waitingCustomers.Count > 0)
                {
                    waitingCustomers.Pop();
                    notOutOfStockEvent.Set();
                }
            }
            finally
            {
                Monitor.Exit(@lock);
            }
        }
    }

    /// <summary>
    /// Simulates a client purchasing a shoe once in a while.
    /// </summary>
    /// <param name="cancelToken">Cancellation token in case we close the shop.</param>
    private void DequeueShoes(CancellationToken cancelToken)
    {
        int timeInMillis = 500 + Generator.Random.Next(2500);

        while (!cancelToken.IsCancellationRequested)
        {
            Thread.Sleep(timeInMillis);
            try
            {
                Monitor.Enter(@lock);
                if (ShoesQueue.Count <= 0)
                {
                    waitingCustomers.Push(true);
                    Console.WriteLine("Client waits for a shoe");
                    Monitor.Exit(@lock);
                    notOutOfStockEvent.WaitOne();
                    Monitor.Enter(@lock);
                }

                string shoe = ShoesQueue.Dequeue();
                Console.WriteLine($"A client has bought {shoe}, rendering the shoes amount to {ShoesQueue.Count}");
                if (waitingDeliveries.Count > 0)
                {
                    waitingDeliveries.Pop();
                    notFullStockEvent.Set();
                }

            }
            finally
            {
                Monitor.Exit(@lock);
            }
        }
    }
}

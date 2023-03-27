using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SeventhLab.Infrastructure;
using SeventhLab.ShoeMakers;

namespace SeventhLab
{
    public class ShoeShop
    {
        private readonly CancellationTokenSource cancellationTokenSource = new();
        private const int MaxQueueSize = 5;
        private readonly int clientelle;
        private readonly AutoResetEvent notOutOfStockEvent = new(true);
        private readonly AutoResetEvent notFullStockEvent = new(false);
        private bool fullStockForTheFirstTime = true;

        public List<IShoeMaker> ShoeMakers { get; } = new();
        public ConcurrentQueue<string> ShoesQueue { get; } = new();

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
                if (ShoesQueue.Count >= MaxQueueSize)
                {
                    if (fullStockForTheFirstTime)
                    {
                        notFullStockEvent.Reset();
                        fullStockForTheFirstTime = false;
                    }

                    Console.WriteLine($"Depot is full. {shoeMaker.Brand} shoes are suspended.");
                    notFullStockEvent.WaitOne();
                }
                
                ShoesQueue.Enqueue(shoe);
                Console.WriteLine($"Shoe {shoe} has been delivered and stored, stocking shoes up to {ShoesQueue.Count}");
                notOutOfStockEvent.Set();
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
                if (ShoesQueue.IsEmpty)
                {
                    Console.WriteLine("Client waits for a shoe");
                    notOutOfStockEvent.WaitOne();
                }

                if (ShoesQueue.TryDequeue(out string? shoe))
                {
                    Console.WriteLine($"A client has bought {shoe}, rendering the shoes amount to {ShoesQueue.Count}");
                    notFullStockEvent.Set();
                }
            }
        }
    }
}

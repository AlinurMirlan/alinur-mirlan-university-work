using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLab
{
    public class CashRegister
    {
        public decimal GetCost()
        {
            Thread totalCostFetchProcess = FetchTotalCost();
            Thread discountFetchProcess = FetchDiscountRate();
            JoinThreads(totalCostFetchProcess, discountFetchProcess);
            return totalCost * (decimal)discountRate;
        }

        #region private members
        private static readonly Random random = new();

        private decimal totalCost;

        private double discountRate;

        private Thread FetchTotalCost()
        {
            Thread totalCostFetchProcess = new(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.5));
                totalCost = (decimal)random.NextDouble() * 1000;
            });

            return totalCostFetchProcess;
        }

        private Thread FetchDiscountRate()
        {
            Thread discountFetchProcess = new(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                discountRate = random.NextDouble();
            });

            return discountFetchProcess;
        }

        private void JoinThreads(params Thread[] threads)
        {
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        #endregion
    }
}

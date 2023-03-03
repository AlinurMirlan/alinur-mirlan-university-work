using System;
using System.Threading;

namespace MultithreadingLocks
{
    public class Balance
    {
        private const long MaxAllowedAmount = 100_000;
        private const long MaxAmountPerTransaction = 10_000;
        private readonly object addAmountLock = new();
        private readonly object withdrawAmountLock = new();

        public long Amount { get; private set; } = 0;

        public Balance(long initialAmount)
        {
            if (initialAmount < 0)
            {
                throw new ArgumentException("The amount should be equal or greater than 0.");
            }

            if (initialAmount > MaxAllowedAmount)
            {
                throw new ArgumentException("The amount surpassed the account limit.");
            }

            Amount = initialAmount;
        }

        public void Add(int amountToAdd)
        {
            if (amountToAdd <= 0)
            {
                throw new ArgumentException("The amount should be greater than 0.");
            }

            if (amountToAdd > MaxAmountPerTransaction)
            {
                throw new ArgumentException($"The value {amountToAdd} exceeds transaction limit: {MaxAmountPerTransaction}.");
            }

            lock (addAmountLock)
            {
                if (Amount + amountToAdd > MaxAllowedAmount)
                {
                    throw new ArgumentException("Cannot add the specified amount: the sum exceeds account limit.");
                }

                AddAmountAndEmulateTransactionDelay(amountToAdd);
            }
        }

        public void Withdraw(int amountToWithdraw)
        {
            if (amountToWithdraw <= 0)
            {
                throw new ArgumentException("The amount should be greater than 0.");
            }

            if (amountToWithdraw > MaxAmountPerTransaction)
            {
                throw new ArgumentException($"The value {amountToWithdraw} exceeds transaction limit: {MaxAmountPerTransaction}.");
            }

            lock (withdrawAmountLock)
            {
                if (amountToWithdraw > Amount)
                {
                    throw new ArgumentException("Insufficient funds.");
                }

                WithdrawAndEmulateTransactionDelay(amountToWithdraw);
            }
        }

        #region private methods

        private void AddAmountAndEmulateTransactionDelay(int amountToAdd)
        {
            Thread.Sleep(1000);
            Amount += amountToAdd;
        }

        private void WithdrawAndEmulateTransactionDelay(int amountToWithdraw)
        {
            Thread.Sleep(1000);
            Amount -= amountToWithdraw;
        }

        #endregion
    }
}

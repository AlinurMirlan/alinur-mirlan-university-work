using System;
using System.Collections.Generic;

namespace divisibility_d
{
    internal class IntDescendComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y.CompareTo(x);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int divider = int.Parse(Console.ReadLine());
            string numberString = Console.ReadLine();
            int[] digits = new int[numberString.Length];
            for (int i = 0; i < numberString.Length; i++)
            {
                digits[i] = (int)char.GetNumericValue(numberString[i]);
            }
            
            if (divider == 2 || divider == 5)
            {
                int minDigit = int.MaxValue;
                int minDigitIndex = -1;
                for (int i = 0; i < digits.Length; i++)
                {
                    if (digits[i] == 0)
                    {
                        Array.Sort(digits, new IntDescendComparer());
                        PrintNumber(digits);
                        return;
                    }
                    else if (digits[i] % divider == 0 && digits[i] < minDigit)
                    {
                        minDigit = digits[i];
                        minDigitIndex = i;
                    }
                }

                if (minDigitIndex != -1)
                {
                    (digits[minDigitIndex], digits[^1]) = (digits[^1], digits[minDigitIndex]);
                    Array.Sort(digits, 0, digits.Length - 1, new IntDescendComparer());
                    PrintNumber(digits);
                }
                else
                {
                    Console.WriteLine(-1);
                }
            }
            else if (divider == 3)
            {
                int sum = 0;
                foreach (int digit in digits)
                {
                    sum += digit;
                }

                if (sum % 3 == 0)
                {
                    Array.Sort(digits, new IntDescendComparer());
                    PrintNumber(digits);
                }
                else
                {
                    Console.WriteLine(-1);
                }
            }
        }

        private static void PrintNumber(int[] digits)
        {
            foreach (int digit in digits)
            {
                Console.Write(digit);
            }
        }
    }
}
using System;

namespace parity_a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string numberString = Console.ReadLine();
            int[] digits = new int[numberString.Length];
            for (int i = 0; i < numberString.Length; i++)
            {
                digits[i] = (int)char.GetNumericValue(numberString[i]);
            }

            int amountOfOddDigits = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                int amountOfOnes = 0;
                string binaryRepresentation = Convert.ToString(digits[i], 2);
                foreach (char binary in binaryRepresentation)
                {
                    if (binary == '1')
                    {
                        amountOfOnes++;
                    }
                }

                if (amountOfOnes % 2 == 1)
                {
                    amountOfOddDigits++;
                }
            }

            if (amountOfOddDigits % 2 == 0)
            {
                Console.WriteLine("EVEN");
            }
            else
            {
                Console.WriteLine("ODD");
            }
        }
    }
}
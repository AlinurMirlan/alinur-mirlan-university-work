using System;
using System.Linq;

namespace prime_f
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
            int gcd = EuclidsAlgorithm(numbers[0], numbers[1]);
            if (gcd == 1)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }

        public static int EuclidsAlgorithm(params int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = numbers[i] < 0 ? -numbers[i] : numbers[i];
            }

            while (true)
            {
                int minIndex = GetMinIndex(numbers);
                int smallest = numbers[minIndex];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] %= smallest;
                }

                if (numbers.All(n => n == 0))
                {
                    return smallest;
                }
                numbers[minIndex] = smallest;
            }
        }

        public static int GetMinIndex(int[] array)
        {
            int minIndex = -1;
            int min = int.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 0)
                    continue;

                if (min > array[i])
                {
                    min = array[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}
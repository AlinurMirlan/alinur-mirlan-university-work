using System;

namespace binary_serach_time_complexity_h
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine(Math.Ceiling(Math.Log2(number)));
        }
    }
}

/*        private static int BinarySearchTimeComplexity(long num)
        {
            long left = 1;
            long right = num;
            int iterations = 0;
            while (right >= left)
            {
                long middle = (right + left) / 2;
                left = middle + 1;
                iterations++;
            }

            return iterations;
        }*/
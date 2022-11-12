using System;
using System.Linq;

namespace guess_a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long[] input = Console.ReadLine().Split(' ').Select(s => long.Parse(s)).ToArray();

            Console.WriteLine(BinarySearch(input[0], input[1]));
        }

        private static int BinarySearch(long num, long find)
        {
            long left = 1;
            long right = num;
            int iterations = 1;
            while (right >= left)
            {
                long middle = (right + left) / 2;
                if (middle == find)
                    return iterations;
                else if (middle > find)
                    right = middle - 1;
                else
                    left = middle + 1;

                iterations++;
            }

            return -1;
        }
    }
}

/* 
 *         // Binary search. Only works when a list is in sorted order. It takes O(log2(n))
        static int? BinarySearch(int[] array, int toFind)
        {
            int leftBoundary = 0;
            int rightBoundary = array.Length - 1;
            while (rightBoundary >= leftBoundary)
            {
                int middleIndex = (rightBoundary + leftBoundary) / 2;
                if (toFind == array[middleIndex])
                {
                    return middleIndex;
                }
                if (toFind > array[middleIndex])
                {
                    leftBoundary = middleIndex + 1;
                }
                else
                {
                    rightBoundary = middleIndex - 1;
                }
            }
            return null;
        }

         public static int BinarySearchNum(int number, int find)
        {
            int left = 1;
            int right = number;
            int middle = number / 2;
            int iterations = 1;
            while (right >= left)
            {
                if (middle == find)
                    return iterations;
                else if (middle > find)
                    right = middle;
                else
                    left = middle;

                middle = (right - left) / 2;
                iterations++;
            }

            return -1;
        }*/
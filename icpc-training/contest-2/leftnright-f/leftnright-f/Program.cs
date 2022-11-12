using System;
using System.Linq;

namespace leftnright_f
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
            int[] array = new int[input[0]];
            int[] toFind = new int[input[1]];
            string[] arrayString = Console.ReadLine().Split(' ');
            for (int i = 0; i < input[0]; i++)
            {
                array[i] = int.Parse(arrayString[i]);
            }

            string[] toFindString = Console.ReadLine().Split(' ');
            for (int i = 0; i < input[1]; i++)
            {
                toFind[i] = int.Parse(toFindString[i]);
            }

            foreach (int find in toFind)
            {
                (int first, int last)? index = BinarySearch(array, find);
                if (index is null)
                    Console.WriteLine("0");
                else
                    Console.WriteLine($"{index.Value.first} {index.Value.last}");
            }
        }

        static (int, int)? BinarySearch(int[] array, int toFind)
        {
            int leftBoundary = 0;
            int rightBoundary = array.Length - 1;
            while (rightBoundary >= leftBoundary)
            {
                int middleIndex = (rightBoundary + leftBoundary) / 2;
                if (toFind == array[middleIndex])
                {
                    int found = array[middleIndex];
                    int firstOccurenceIndex, lastOccurenceIndex;
                    int j = 1;
                    for (int i = middleIndex - j; i >= 0 && array[i] == found; i--) 
                        j++;
                    firstOccurenceIndex = middleIndex - j + 1;

                    j = 1;
                    for (int i = middleIndex + j; i < array.Length && array[i] == found; i++) 
                        j++;
                    lastOccurenceIndex = middleIndex + j - 1;

                    return (firstOccurenceIndex + 1, lastOccurenceIndex + 1);
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
    }
}
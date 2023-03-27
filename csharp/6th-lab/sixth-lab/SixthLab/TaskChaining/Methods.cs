using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLab.TaskChaining
{
    public static class Methods
    {
        private static readonly Random random = new();

        public static int[] CreateArrayOfTenIntegers()
        {
            return Enumerable.Range(1, 10).Select(n => random.Next(1, 100)).ToArray();
        }

        public static int[] MultiplyArrayByRandom(int[] array)
        {
            int coefficient = random.Next(1, 100);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] *= coefficient;
            }
            return array;
        }

        public static int[] SortByAscending(int[] array)
        {
            return array.OrderBy(number => number).ToArray();
        }

        public static double CalculateAverage(int[] array)
        {
            return (double)array.Aggregate((current, next) => current + next) / array.Length;
        }
    }
}

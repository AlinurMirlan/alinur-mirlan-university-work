using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLab.TaskChaining
{
    public static class ArrayManipulation
    {
        public static void Process()
        {
            Task<int[]> createArray = Task.Factory.StartNew(
                CreateArray,
                TaskCreationOptions.DenyChildAttach);
            SimulateWork(createArray);

            Task<int[]> multiplyArray = createArray.ContinueWith(
                antecedent =>
                {
                    return MultiplyBy(antecedent.Result);
                }, TaskContinuationOptions.OnlyOnRanToCompletion);
            SimulateWork(multiplyArray);

            Task<int[]> sortArray = multiplyArray.ContinueWith(
                antecedent => Sort(antecedent.Result),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            SimulateWork(sortArray);

            Task<double> averageArray = sortArray.ContinueWith(
                antecedent => Average(antecedent.Result),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            SimulateWork(averageArray);
        }

        private static void SimulateWork(Task task)
        {
            while (task.Status != TaskStatus.RanToCompletion)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
        }

        private static int[] CreateArray()
        {
            Console.WriteLine("Creating array");

            var array = Methods.CreateArrayOfTenIntegers();
            // Simulating some heavy computational work.
            Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.WriteLine("\nCreated an array of ten elements");
            Console.WriteLine(array.ToNumberString() + "\n");
            return array;
        }
        private static int[] MultiplyBy(int[] array)
        {
            Console.WriteLine("Multiplying by a number");

            var oldArray = (int[])array.Clone();
            var multipliedArray = Methods.MultiplyArrayByRandom(array);
            // Simulating some heavy computational work.
            Thread.Sleep(TimeSpan.FromSeconds(1));

            var coefficient = CalculateCoefficient(oldArray, multipliedArray);
            Console.WriteLine($"\nMultiplied by {coefficient}\n{multipliedArray.ToNumberString()}\n");
            return multipliedArray;
        }

        private static int[] Sort(int[] array)
        {
            Console.WriteLine("Sorting");

            // Simulating some heavy computational work.
            var sortedArray = Methods.SortByAscending(array);
            Thread.Sleep(TimeSpan.FromSeconds(4));

            Console.WriteLine($"\nArray sorted by ascending\n{sortedArray.ToNumberString()}\n");
            return sortedArray;
        }

        private static double Average(int[] array)
        {
            Console.WriteLine("Calculating average");

            // Simulating some heavy computational work.
            Thread.Sleep(TimeSpan.FromSeconds(.5));
            double average = Methods.CalculateAverage(array);

            Console.WriteLine($"\nObtained average of {average}\n");
            return average;
        }

        private static int CalculateCoefficient(int[] oldArray, int[] newArray)
        {
            var difference = newArray[0] - oldArray[0];
            var coefficient = (difference / oldArray[0]) + 1;
            return coefficient;
        }

        public static string ToNumberString(this int[] array)
        {
            StringBuilder arrayString = new StringBuilder();
            foreach (int number in array)
            {
                arrayString.Append($"{number}, ");
            }
            return arrayString.ToString()[..^2];
        }

    }
}

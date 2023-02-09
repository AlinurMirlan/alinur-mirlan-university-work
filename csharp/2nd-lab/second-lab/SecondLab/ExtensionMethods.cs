using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab
{
    internal static class ExtensionMethods
    {
        public static void Display<T>(this IEnumerable<T> enumerable, string? message = null)
        {
            if (message is not null)
                Console.WriteLine(message);

            Console.Write("Elements: ");
            foreach (T element in enumerable)
                Console.Write($"{element} ");
            Console.WriteLine();
        }
    }
}

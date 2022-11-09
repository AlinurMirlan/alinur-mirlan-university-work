using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace encryption_a
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char commonChar = Console.ReadLine()[0];
            int m = int.Parse(Console.ReadLine());
            int[] codes = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
            int mostFrequentCode = FindFrequent(codes);
            int commonCharCode = commonChar;
            string mostFrequentBinary = Convert.ToString(mostFrequentCode, 2);
            string commonCharBinary = Convert.ToString(commonCharCode, 2);
            int commonLength = Math.Max(mostFrequentBinary.Length, commonCharBinary.Length);
            mostFrequentBinary = mostFrequentBinary.PadLeft(commonLength, '0');
            commonCharBinary = commonCharBinary.PadLeft(commonLength, '0');
            StringBuilder keyBinaryBuilder = new();
            for (int i = 0; i < commonLength; i++)
            {
                if (mostFrequentBinary[i] != commonCharBinary[i])
                    keyBinaryBuilder.Append('1');
                else
                    keyBinaryBuilder.Append('0');
            }

            int key = Convert.ToInt32(keyBinaryBuilder.ToString(), 2);
            foreach (int code in codes)
            {
                char symbol = (char)(code ^ key);
                Console.Write(symbol);
            }
        }

        private static int FindFrequent(int[] array)
        {
            Dictionary<int, int> count = new();
            foreach (int element in array)
            {
                if (count.ContainsKey(element))
                    count[element] += 1;
                else
                    count[element] = 1;
            }

            int maxOccurence = -1;
            int frequentElement = -1;
            foreach (var pair in count)
            {
                if (pair.Value > maxOccurence)
                {
                    frequentElement = pair.Key;
                    maxOccurence = pair.Value;
                }
            }

            return frequentElement;
        }
    }
}

/*
            

            string textToEncode = "helloworld";
            Console.WriteLine($"{textToEncode}\n");
            foreach (var symbol in textToEncode)
            {
                Console.Write(Convert.ToString(symbolCodes[symbol], 2) + " ");
            }

            Console.WriteLine();
            foreach (var symbol in textToEncode)
            {
                Console.Write(symbolCodes[symbol] + " ");
            }*/
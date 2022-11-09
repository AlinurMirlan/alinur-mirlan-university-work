using System;
using System.Text;
using System.Collections.Generic;

namespace roman_arithmetic_n
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string left = Console.ReadLine();
            string right = Console.ReadLine();
            int sum = GetArabicFromRoman(left) + GetArabicFromRoman(right);
            Console.WriteLine(GetRomanFromArabic(sum));
        }

        private static string GetRomanFromArabic(int arabicNumber)
        {
            (int numeral, string symbol)[] arabicToRoman = new (int, string)[12]
            {
                (1000, "M"),
                (900, "CM"),
                (500, "D"),
                (100, "C"),
                (90, "XC"),
                (50, "L"),
                (40, "XL"),
                (10, "X"),
                (9, "IX"),
                (5, "V"),
                (4, "IV"),
                (1, "I"),
            };

            StringBuilder romanStringBuilder = new();
            foreach (var numberToRoman in arabicToRoman)
            {
                if (arabicNumber >= numberToRoman.numeral)
                {
                    int count = arabicNumber / numberToRoman.numeral;
                    arabicNumber -= count * numberToRoman.numeral;
                    for (int i = 0; i < count; i++)
                        romanStringBuilder.Append(numberToRoman.symbol);
                }
            }

            return romanStringBuilder.ToString();
        }

        private static int GetArabicFromRoman(string romanNumber)
        {
            Dictionary<char, int> romanToArabic = new()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
            };

            int previous = int.MinValue;
            int sum = 0;
            for (int i = 1; i <= romanNumber.Length; i++)
            {
                char symbol = romanNumber[^i];
                int arabicNumber = romanToArabic[symbol];
                if (previous <= arabicNumber)
                    sum += arabicNumber;
                else
                    sum -= arabicNumber;

                previous = arabicNumber;
            }

            return sum;
        }
    }
}
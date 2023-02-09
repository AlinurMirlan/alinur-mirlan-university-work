using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondLab
{
    internal static class Utility
    {
        private readonly static Random random = new();

        public static ICollection<string> GenerateStrings(int amount)
        {
            List<string> list = new();
            for (int i = 0; i < amount; i++)
                list.Add(GenerateString());

            return list;
        }

        public static ICollection<string> GenerateStrings(int amount, char separator)
        {
            List<string> list = new();
            for (int i = 0; i < amount; i++)
            {
                int wordsCount = random.Next(2) + 1;
                string sentence = Enumerable.Range(0, wordsCount).Aggregate(string.Empty, (total, _) => total + GenerateString(isUpperCase: true) + separator);
                sentence = sentence[..^1];
                list.Add(sentence);
            }

            return list;
        }

        private static string GenerateString(bool isUpperCase = false)
        {
            string alphabet = isUpperCase ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "abcdefghijklmnopqrstuvwxyz";
            StringBuilder stringBuilder = new();
            int length = random.Next(12) + 1;
            for (int i = 0; i < length; i++)
                stringBuilder.Append(alphabet[random.Next(alphabet.Length)]);

            return stringBuilder.ToString();
        }
    }
}

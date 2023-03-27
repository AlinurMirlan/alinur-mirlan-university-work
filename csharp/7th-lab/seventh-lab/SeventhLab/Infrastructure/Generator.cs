using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventhLab.Infrastructure
{
    public static class Generator
    {
        private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static readonly Random Random = new();

        public static string GenerateSerialNumber()
        {
            int length = 3 + Random.Next(5);
            string letterCode = Enumerable.Range(0, length).Aggregate(string.Empty, (string letterCode, int _) => letterCode + alphabet[Random.Next(alphabet.Length)]);
            return $"{Random.Next(99999),5}{letterCode}";
        }
    }
}

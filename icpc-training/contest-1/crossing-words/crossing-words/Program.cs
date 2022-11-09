using System.Text;
using System;
using System.Collections.Generic;

namespace crossing_words
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] sentences = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            (LinkedList<string> lines, int verticalUpperLength, int verticalLowerLength, int horizontalLength)?[] crossWords = { CrossWords(sentences[0], sentences[1]), CrossWords(sentences[2], sentences[3]) };

            int maxUpperLength = 0;
            int maxLowerLength = 0;
            foreach (var crossWord in crossWords)
            {
                if (crossWord == null)
                {
                    Console.WriteLine("Unable to make two crosses");
                    return;
                }

                maxUpperLength = Math.Max(crossWord.Value.verticalUpperLength, maxUpperLength);
                maxLowerLength = Math.Max(crossWord.Value.verticalLowerLength, maxLowerLength);
            }

            foreach (var crossWord in crossWords)
            {
                for (int i = 0; i < maxUpperLength - crossWord.Value.verticalUpperLength; i++)
                    crossWord.Value.lines.AddFirst(string.Empty.PadLeft(crossWord.Value.horizontalLength, ' '));

                for (int i = 0; i < maxLowerLength - crossWord.Value.verticalLowerLength; i++)
                    crossWord.Value.lines.AddLast(string.Empty.PadLeft(crossWord.Value.horizontalLength, ' '));
            }

            StringBuilder[] doubleCross = new StringBuilder[maxLowerLength + maxUpperLength];
            for (int i = 0; i < maxLowerLength + maxUpperLength; i++)
                doubleCross[i] = new StringBuilder();

            foreach (var crossWord in crossWords)
            {
                var node = crossWord.Value.lines.First;
                for (int i = 0; i < doubleCross.Length; i++)
                {
                    doubleCross[i].Append($"{node.Value}   ");
                    node = node.Next;
                }
            }

            foreach (var builder in doubleCross)
                Console.WriteLine(builder.ToString().TrimEnd());
        }

        private static (LinkedList<string>, int verticalUpperLength, int verticalLowerLength, int horizontalLength)? CrossWords(string left, string right)
        {
            int? length = null;
            int crossPoint = 0;
            Action loops = () =>
            {
                for (int i = 0; i < left.Length; i++)
                {
                    for (int j = 0; j < right.Length; j++)
                    {
                        if (char.ToLower(left[i]) == char.ToLower(right[j]))
                        {
                            length = i;
                            crossPoint = j;
                            return;
                        }
                    }
                }
            };
            loops();

            LinkedList<string> crossWord = new();
            if (length != null)
            {
                for (int i = 0; i < crossPoint; i++)
                    crossWord.AddLast(right[i..(i + 1)].PadLeft(length.Value + 1, ' ').PadRight(left.Length));

                crossWord.AddLast(left);
                for (int i = crossPoint + 1; i < right.Length; i++)
                    crossWord.AddLast(right[i..(i + 1)].PadLeft(length.Value + 1, ' ').PadRight(left.Length));

                return (crossWord, crossPoint, right.Length - crossPoint, left.Length);
            }

            return null;
        }
    }
}
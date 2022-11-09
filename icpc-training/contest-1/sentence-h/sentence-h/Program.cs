using System;
using System.Collections.Generic;

namespace sentence_h
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] sentence = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(FindHammingDistance(sentence));
        }

        public static int FindHammingDistance(string[] tokens)
        {
            LinkedList<string> list = new(tokens);

            int hammingDistance = 0;
            LinkedListNode<string> outerNode = list.First;
            while (outerNode != null)
            {
                int coefficient = 1;
                int localHammingDistance = 0;
                int supplement = 0;
                bool duplicateEncounteredForTheFirstTime = true;
                LinkedListNode<string> innerNode = outerNode.Next;
                while (innerNode != null)
                {
                    string outerString = outerNode.Value;
                    string innerString = innerNode.Value;
                    if (outerString == innerString)
                    {
                        if (duplicateEncounteredForTheFirstTime)
                        {
                            supplement = localHammingDistance;
                            duplicateEncounteredForTheFirstTime = false;
                        }
                        else
                            supplement += localHammingDistance / coefficient - supplement;

                        coefficient++;
                        localHammingDistance += supplement;
                        LinkedListNode<string> nextNode = innerNode.Next;
                        list.Remove(innerNode);
                        innerNode = nextNode;
                        continue;
                    }

                    int biggerLength, smallerLength;
                    if (outerString.Length > innerString.Length)
                    {
                        biggerLength = outerString.Length;
                        smallerLength = innerString.Length;
                    }
                    else
                    {
                        biggerLength = innerString.Length;
                        smallerLength = outerString.Length;
                    }

                    for (int k = 0; k < smallerLength; k++)
                    {
                        if (outerString[k] != innerString[k])
                            localHammingDistance += coefficient;
                    }

                    localHammingDistance += (biggerLength - smallerLength) * coefficient;
                    innerNode = innerNode.Next;
                }

                hammingDistance += localHammingDistance;
                outerNode = outerNode.Next;
            }

            return hammingDistance;
        }
    }
}



/*using System;
using System.Collections.Generic;

namespace sentence_h
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int hammingDistance = 0;
            Dictionary<string, int> hammingDistanceOfToken = new();
            for (int i = 0; i < tokens.Length; i++)
            {
                for (int j = i + 1; j < tokens.Length; j++)
                {
                    int biggerLength, smallerLength;
                    if (tokens[i].Length > tokens[j].Length)
                    {
                        biggerLength = tokens[i].Length;
                        smallerLength = tokens[j].Length;
                    }
                    else
                    {
                        biggerLength = tokens[j].Length;
                        smallerLength = tokens[i].Length;
                    }

                    for (int k = 0; k < smallerLength; k++)
                    {
                        if (tokens[i][k] != tokens[j][k])
                            hammingDistance++;
                    }

                    hammingDistance += biggerLength - smallerLength;
                }
            }

            Console.WriteLine(hammingDistance);
        }
    }
}*/





/*
 using System;
using System.Collections.Generic;

namespace sentence_h
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int hammingDistance = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                Dictionary<string, int> processedDistance = new();
                for (int j = i + 1; j < tokens.Length; j++)
                {
                    if (processedDistance.ContainsKey(tokens[j]))
                    {
                        hammingDistance += processedDistance[tokens[j]];
                        continue;
                    }

                    int paddedToken = 1;
                    string[] localTokens = new string[2];
                    if (tokens[i].Length > tokens[j].Length)
                    {
                        localTokens[0] = tokens[i];
                        localTokens[1] = tokens[j].PadRight(tokens[i].Length, ' ');
                    }
                    else
                    {
                        paddedToken = 0;
                        localTokens[0] = tokens[i].PadRight(tokens[j].Length, ' ');
                        localTokens[1] = tokens[j];
                    }

                    int localHammingDistance = 0;
                    for (int k = 0; k < localTokens[0].Length; k++)
                    {
                        if (localTokens[paddedToken][k] == ' ')
                        {
                            localHammingDistance += localTokens[0].Length - k;
                            break;
                        }

                        if (localTokens[0][k] != localTokens[1][k])
                            localHammingDistance++;
                    }

                    hammingDistance += localHammingDistance;
                    processedDistance.Add(tokens[j], localHammingDistance);
                }
            }

            Console.WriteLine(hammingDistance);
        }
    }
}*/

/*
         public static int FindHammingDistanceDigits(int[] nums)
        {
            LinkedList<string> list = new(nums.Select(n => Convert.ToString(n, 2)));

            int hammingDistance = 0;
            LinkedListNode<string> outerNode = list.First;
            while (outerNode != null)
            {
                int coefficient = 1;
                int localHammingDistance = 0;
                int supplement = 0;
                bool duplicateEncounteredForTheFirstTime = true;
                LinkedListNode<string> innerNode = outerNode.Next;
                while (innerNode != null)
                {
                    string outerString = outerNode.Value;
                    string innerString = innerNode.Value;
                    if (outerString == innerString)
                    {
                        if (duplicateEncounteredForTheFirstTime)
                        {
                            supplement = localHammingDistance;
                            duplicateEncounteredForTheFirstTime = false;
                        }
                        else
                            supplement += localHammingDistance / coefficient - supplement;

                        coefficient++;
                        localHammingDistance += supplement;
                        LinkedListNode<string> nextNode = innerNode.Next;
                        list.Remove(innerNode);
                        innerNode = nextNode;
                        continue;
                    }

                    int commonLength = Math.Max(outerString.Length, innerString.Length);
                    outerString = outerString.PadLeft(commonLength, '0');
                    innerString = innerString.PadLeft(commonLength, '0');

                    for (int k = 0; k < commonLength; k++)
                    {
                        if (outerString[k] != innerString[k])
                            localHammingDistance += coefficient;
                    }

                    innerNode = innerNode.Next;
                }

                hammingDistance += localHammingDistance;
                outerNode = outerNode.Next;
            }

            return hammingDistance;
        }
 */
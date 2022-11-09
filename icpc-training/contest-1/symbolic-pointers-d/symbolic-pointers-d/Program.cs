using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace symbolic_pointers_d
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            StringBuilder[] resultingPointers = new StringBuilder[n];
            for (int i = 0; i < n; i++)
            {
                string[] paths = Console.ReadLine().Split(" -> ");
                resultingPointers[i] = new StringBuilder($"{paths[0]} -> ");
                if (paths[1][0] == '/')
                {
                    int slashCount = paths[0].Count(s => s == '/');
                    for (int j = 0; j < slashCount; j++)
                        resultingPointers[i].Append("../");

                    resultingPointers[i].Remove(resultingPointers[i].Length - 1, 1);
                    resultingPointers[i].Append(paths[1]);
                }
                else
                {
                    LinkedList<string> relativePathTokens = new();
                    foreach (string token in paths[1].Split('/'))
                        relativePathTokens.AddLast(token);

                    var node = relativePathTokens.First;
                    while (node != null)
                    {
                        if (node.Value == ".." && node.Previous != null && node.Previous.Value != "..")
                        {
                            node = RemoveLastTwoNodes(relativePathTokens, node);
                            continue;
                        }

                        node = node.Next;
                    }

                    Stack<string> absolutePathStack = new();
                    Stack<string> poppedTokens = new();
                    string[] absolutePath = paths[0].Split('/');
                    foreach (string token in absolutePath)
                        absolutePathStack.Push(token);

                    node = relativePathTokens.First;
                    while (node != null)
                    {
                        if (absolutePathStack.TryPop(out string token) && node.Value == "..")
                            poppedTokens.Push(token);
                        else if (poppedTokens.Count != 0 && node.Value == poppedTokens.Pop())
                        {
                            node = RemoveLastTwoNodes(relativePathTokens, node);
                            continue;
                        }

                        node = node.Next;
                    }

                    foreach (string token in relativePathTokens)
                        resultingPointers[i].Append($"{token}/");

                    resultingPointers[i].Remove(resultingPointers[i].Length - 1, 1);
                }
            }

            foreach (StringBuilder pointer in resultingPointers)
                Console.WriteLine(pointer.ToString());
        }

        private static LinkedListNode<T> RemoveLastTwoNodes<T>(LinkedList<T> list, LinkedListNode<T> node)
        {
            var previousNode = node.Previous;
            var nextNode = node.Next;
            list.Remove(node);
            list.Remove(previousNode);
            return nextNode;
        }
    }
}

/*                    foreach (string token in relativeTokens)
                    {
                        if (token == "..")
                        {
                            while (resultingPointers[i][^1] != '/')
                                resultingPointers[i].Remove(resultingPointers[i].Length - 1, 1);

                            resultingPointers[i].Remove(resultingPointers[i].Length - 1, 1);
                        }
                        else
                            resultingPointers[i].Append($"/{token}");
                    }*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace little_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfPairs = int.Parse(Console.ReadLine());
            LinkedList<int[]> rectangles = new();
            int numberOfPunies = 0;

            for (int i = 0; i < numberOfPairs; i++)
            {
                int[] pair = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
                LinkedListNode<int[]> node = rectangles.First;
                bool isConstituent = false;
                while (node != null)
                {
                    int[] rectangle = node.Value;
                    bool leftSmaller = pair[0] <= rectangle[0];
                    bool rightSmaller = pair[1] <= rectangle[1];
                    bool leftSmallerReverse = pair[1] <= rectangle[0];
                    bool rightSmallerReverse = pair[0] <= rectangle[1];
                    if ((leftSmaller && rightSmaller) || (leftSmallerReverse && rightSmallerReverse))
                    {
                        numberOfPunies++;
                        isConstituent = true;
                        break;
                    }

                    bool leftLargerOrEqual = pair[0] >= rectangle[0];
                    bool rightLargerOrEqual = pair[1] >= rectangle[1];
                    bool leftLargerReverse = pair[1] >= rectangle[0];
                    bool rightLargerReverse = pair[0] >= rectangle[1];
                    if ((leftLargerOrEqual && rightLargerOrEqual) || (leftLargerReverse && rightLargerReverse))
                    {
                        numberOfPunies++;
                        LinkedListNode<int[]> nextNode = node.Next;
                        rectangles.Remove(node);
                        node = nextNode;
                        continue;
                    }

                    node = node.Next;
                }

                if (!isConstituent)
                {
                    rectangles.AddLast(pair);
                }
            }

            Console.WriteLine(numberOfPunies);
        }
    }
}



/*foreach (var rectangle in rectangles)
{
    bool leftSmaller = pair[0] <= rectangle[0];
    bool rightSmaller = pair[1] <= rectangle[1];
    bool leftSmallerReverse = pair[0] <= rectangle[1];
    bool rightSmallerReverse = pair[1] <= rectangle[0];
    if (leftSmaller && rightSmaller || (leftSmallerReverse && rightSmallerReverse))
    {
        numberOfPunies++;
        break;
    }

    bool leftLargerOrEqual = pair[0] >= rectangle.Key[0];
    bool rightLargerOrEqual = pair[1] >= rectangle.Key[1];
    bool leftLargerReverse = pair[0] >= rectangle.Key[1];
    bool rightLargerReverse = pair[1] >= rectangle.Key[0];
    if ((((!leftSmaller && rightLargerOrEqual) || (leftLargerOrEqual && !rightSmaller))
        || ((!leftSmallerReverse && rightLargerReverse) || (leftLargerReverse && !rightSmallerReverse)))
        && !rectangle.Value)
    {
        numberOfPunies++;
        rectangles[rectangle.Key] = true;
    }
}
rectangles.Add(pair);*/
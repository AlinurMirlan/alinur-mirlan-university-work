using System;
using System.Linq;

namespace minesweeper_f
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] fieldDimen = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();
            char[,] field = new char[fieldDimen[0], fieldDimen[1]];

            int mines = int.Parse(Console.ReadLine());
            for (int i = 0; i < fieldDimen[0]; i++)
            {
                for (int j = 0; j < fieldDimen[1]; j++)
                {
                    field[i, j] = '0';
                }
            }

            for (int i = 0; i < mines; i++)
            {
                int[] mineCoord = Console.ReadLine().Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                field[mineCoord[0], mineCoord[1]] = '*';
                int right = mineCoord[1] + 1;
                int left = mineCoord[1] - 1;
                int top = mineCoord[0] - 1;
                int bottom = mineCoord[0] + 1;
                bool doesRightExist = right < fieldDimen[1];
                bool doesLeftExist = left >= 0;
                bool doesTopExist = top >= 0;
                bool doesBottomExist = bottom < fieldDimen[0];
                int[,] coords = new int[8, 2]
                {
                    { mineCoord[0], right }, { mineCoord[0], left },
                    { top, mineCoord[1] }, { bottom, mineCoord[1] },
                    { top, right }, { top, left },
                    { bottom, right }, { bottom, left },
                };
                bool[] precidates = new bool[8]
                {
                    doesRightExist && field[coords[0, 0], coords[0, 1]] != '*',
                    doesLeftExist && field[coords[1, 0], coords[1, 1]] != '*',
                    doesTopExist && field[coords[2, 0], coords[2, 1]] != '*',
                    doesBottomExist && field[coords[3, 0], coords[3, 1]] != '*',
                    doesTopExist && doesRightExist && field[coords[4, 0], coords[4, 1]] != '*',
                    doesTopExist && doesLeftExist && field[coords[5, 0], coords[5, 1]] != '*',
                    doesBottomExist && doesRightExist && field[coords[6, 0], coords[6, 1]] != '*',
                    doesBottomExist && doesLeftExist && field[coords[7, 0], coords[7, 1]] != '*',
                };
                for (int j = 0; j < 8; j++)
                {
                    if (precidates[j])
                    {
                        field[coords[j, 0], coords[j, 1]] = (char.GetNumericValue(field[coords[j, 0], coords[j, 1]]) + 1).ToString()[0];
                    }
                }
            }

            for (int i = 0; i < fieldDimen[0]; i++)
            {
                for (int j = 0; j < fieldDimen[1]; j++)
                {
                    Console.Write($"{field[i, j]} ");
                }
                Console.WriteLine();
            }

        }
    }
}



/*                if (doesTopExist)
                {
                    if (field[top, mineCoord[1]] != '*')
                    {
                        field[top, mineCoord[1]] = (char.GetNumericValue(field[top, mineCoord[1]]) + 1).ToString()[0];
                    }
                    if (doesRightExist && field[top, right] != '*')
                    {
                        field[top, right] = (char.GetNumericValue(field[top, right]) + 1).ToString()[0];
                    }
                    if (doesLeftExist && field[top, left] != '*')
                    {
                        field[top, left] = (char.GetNumericValue(field[top, left]) + 1).ToString()[0];
                    }
                }
                if (doesRightExist && field[mineCoord[0], right] != '*')
                {
                    field[mineCoord[0], right] = (char.GetNumericValue(field[mineCoord[0], right]) + 1).ToString()[0];
                }
                if (doesLeftExist && field[mineCoord[0], left] != '*')
                {
                    field[mineCoord[0], left] = (char.GetNumericValue(field[mineCoord[0], left]) + 1).ToString()[0];
                }
                if (doesBottomExist)
                {
                    if (field[bottom, mineCoord[1]] != '*')
                    {
                        field[bottom, mineCoord[1]] = (char.GetNumericValue(field[bottom, mineCoord[1]]) + 1).ToString()[0];
                    }
                    if (doesRightExist && field[bottom, right] != '*')
                    {
                        field[bottom, right] = (char.GetNumericValue(field[bottom, right]) + 1).ToString()[0];
                    }
                    if (doesLeftExist && field[bottom, left] != '*')
                    {
                        field[bottom, left] = (char.GetNumericValue(field[bottom, left]) + 1).ToString()[0];
                    }
                }*/
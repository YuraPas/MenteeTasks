using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epam.Exercises.CleanCode.EightQueens.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<char[][]> solutions = new List<char[][]>();

            int length = 8;
            char[][] result = CreateArray(length);

            result = AssignArray(result);

            SolveAllNQueens(result, 0, solutions);

            Console.WriteLine(solutions.Count);
            OuputResults(solutions);

            Console.ReadLine();
        }

        private static void SolveAllNQueens(char[][] board, int col, List<char[][]> solutions)
        {
            int length = board.Length;
            if (col == length)
            {
                char[][] copy = CreateArray(length);
                copy = board.Select(a => a.ToArray()).ToArray();

                solutions.Add(copy);
            }
            else
            {
                for (int row = 0; row < board.Length; row++)
                {
                    board[row][col] = 'Q';
                    bool canBeSafe = true;

                    canBeSafe = Check(board, canBeSafe);

                    canBeSafe = Check(board, canBeSafe, swap: true);

                    canBeSafe = Check(board, canBeSafe, loopStart: -board.Length, inBoundsCheckNeeded: true);

                    canBeSafe = Check(board, canBeSafe, loopStart: -board.Length, inBoundsCheckNeeded: true, sign: -1, offset: board.Length - 1);

                    if (canBeSafe)
                    {
                        SolveAllNQueens(board, col + 1, solutions);
                    }

                    board[row][col] = '.';
                }
            }
        }

        private static bool Check(char[][] board, bool canBeSafe, bool swap = false, int loopStart = 0, bool inBoundsCheckNeeded = false, int sign = 1, int offset = 0)
        {
            int lenght = board.Length;

            for (int i = loopStart; i < lenght; i++)
            {
                bool found = false;

                for (int j = 0; j < lenght; j++)
                {
                    if (inBoundsCheckNeeded)
                    {
                        if (Inbounds(j, (sign * (i + j)) + offset, board))
                        {
                            (found, canBeSafe) = CheckIfEqualsQ(board[j][(sign * (i + j)) + offset], found, canBeSafe);
                        }
                    }
                    else
                    {
                        if (swap)
                        {
                            (found, canBeSafe) = CheckIfEqualsQ(board[j][i], found, canBeSafe);
                        }
                        else
                        {
                            (found, canBeSafe) = CheckIfEqualsQ(board[i][j], found, canBeSafe);
                        }
                    }
                }
            }

            return canBeSafe;
        }

        private static (bool, bool) CheckIfEqualsQ(char item, bool found, bool canBeSafe)
        {
            if (item == 'Q')
            {
                if (found)
                {
                    canBeSafe = false;
                }

                found = true;
            }

            return (found, canBeSafe);
        }

        private static bool Inbounds(int row, int col, char[][] array)
        {
            if (row >= 0 && row < array.Length &&
                col >= 0 && col < array[0].Length)
            {
                return true;
            }

            return false;
        }

        private static void OuputResults(List<char[][]> solutions)
        {
            int count = 1;
            foreach (var solution in solutions)
            {
                Console.WriteLine("\nSolution " + count);

                for (int i = 0; i < solution.Length; i++)
                {
                    for (int y = 0; y < solution[i].Length; y++)
                    {
                        Console.Write(solution[i][y] + " ");
                    }

                    Console.WriteLine();
                }

                count++;
            }
        }

        private static char[][] CreateArray(int lenght)
        {
            var array = new char[lenght][];

            for (int i = 0; i < lenght; i++)
            {
                array[i] = new char[lenght];
            }

            return array;
        }

        private static char[][] AssignArray(char[][] array)
        {
            int length = array.Length;

            for (int i = 0; i < length; i++)
            {
                for (int y = 0; y < length; y++)
                {
                    array[i][y] = '.';
                }
            }

            return array;
        }
    }
}
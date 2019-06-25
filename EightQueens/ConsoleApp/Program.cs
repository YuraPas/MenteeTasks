using System;
using System.Collections.Generic;
using System.Globalization;

namespace Epam.Exercises.CleanCode.EightQueens.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args) {
            List<char[][]> solutions = new List<char[][]>();

            char[][] result = new char[8][];
            for (int r1 = 0; r1 < 8; r1++)
                result[r1] = new char[8];
            for (int r1 = 0; r1 < 8; r1++)
            for (int r2 = 0; r2 < 8; r2++) { result[r1][r2] = '.'; }

            SolveAllNQueens(result, 0, solutions);

            Console.WriteLine(solutions.Count);
            for (int i = 0; i < solutions.Count; i++) {
                Console.WriteLine("\nSolution " + (i + 1));

                char[][] board = solutions[i];

                for (int r = 0; r < board.Length; r++) {
                    for (int c = 0; c < board[r].Length; c++)
                        Console.Write(board[r][c] + " ");
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }

        private static void SolveAllNQueens(char[][] board, int col, List<char[][]> solutions) {
            if (col == board.Length) {
                char[][] copy = new char[board.Length][];
                for (int r = 0; r < board.Length; r++)
                    copy[r] = new char[board[0].Length];
                for (int r = 0; r < board.Length; r++)
                    for (int c = 0; c < board[0].Length; c++)
                        copy[r][c] = board[r][c];
                solutions.Add(copy);
            } else {
                for (int row = 0; row < board.Length; row++) {
                    board[row][col] = 'Q';
                    bool canBeSafe = true;
                    
                    for (int i = 0; i < board.Length; i++) {
                        bool found = false;
                        for (int j = 0; j < board.Length; j++) {
                            if (board[i][j] == 'Q') {
                                if (found) {
                                    canBeSafe = false;
                                }
                                found = true;
                            }
                        }
                    }
                    
                    for (int i = 0; i < board.Length; i++) {
                        bool found = false;
                        for (int j = 0; j < board.Length; j++) {
                            if (board[j][i] == 'Q') {
                                if (found) {
                                    canBeSafe = false;
                                }
                                found = true;
                            }
                        }
                    }
                    
                    for (int offset = -board.Length; offset < board.Length; offset++) {
                        bool found = false;
                        for (int i = 0; i < board.Length; i++) {
                            if (Inbounds(i, i + offset, board)) {
                                if (board[i][i + offset] == 'Q') {
                                    if (found) {
                                        canBeSafe = false;
                                    }
                                    found = true;
                                }
                            }
                        }
                    }

                    for (int offset = -board.Length; offset < board.Length; offset++) {
                        bool found = false;
                        for (int i = 0; i < board.Length; i++) {
                            if (Inbounds(i, board.Length - offset - i - 1, board)) {
                                if (board[i][board.Length - offset - i - 1] == 'Q') {
                                    if (found) {
                                        canBeSafe = false;
                                    }
                                    found = true;
                                }
                            }
                        }
                    }

                    if (canBeSafe)
                        SolveAllNQueens(board, col + 1, solutions);
                    board[row][col] = '.';
                }
            }
        }

        private static bool Inbounds(int row, int col, char[][] mat) {
            return row >= 0 && row < mat.Length && col >= 0 && col < mat[0].Length;
        }

    }
}
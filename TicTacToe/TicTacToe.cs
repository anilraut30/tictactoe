using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace tictactoe
{
    public class TicTacToe
    {
        private int[] rows;
        private int[] cols;
        private int diagonal1; // \
        private int diagonal2; // /
        private int length;
        char[,] board;
        private int movesCount;
        private int compMoveCount;
        private Dictionary<string, string> boardState;
        List<MoveTrack> moveTracks;

        public int? Result { get; set; }

        public TicTacToe(int size)
        {
            rows = new int[size];
            cols = new int[size];
            board = new char[size, size];
            movesCount = 0;
            compMoveCount = 0;
            Result = null;

            moveTracks = new List<MoveTrack>();

            diagonal1 = 0;
            diagonal2 = 0;
            length = size;

        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '\0';
                }
            }
            Result = null;
            compMoveCount = 0;
            movesCount = 0;
        }

        public void computerMove()
        {
            Random rnd = new Random();
            int row = -1, col = -1;

            if (compMoveCount == 0 && board[1, 1] == '\0')
            {
                row = 1;
                col = 1;
            }
            else
            {
                do
                {
                    row = rnd.Next(0, 3);
                    col = rnd.Next(0, 3);

                } while ((board[row, col] == 'X' || board[row, col] == 'O') && compMoveCount < 4);

            }

            board[row, col] = 'O';


            var moveResult = Move(row, col, 0);
            if (compMoveCount >= 5 && moveResult == 0)
                Result = 0;

            if (compMoveCount >= 2)
                Result = moveResult;

            compMoveCount++;
        }

        public int PlayerMove(string pos)
        {

            int row, col;
            char ch;
            if (pos.Length == 2 && int.TryParse(pos.Substring(0, 1), out row) && int.TryParse(pos.Substring(1, 1), out col) && row >= 0 && row <= 2 && col >= 0 && col <= 2)
            {
                ch = board[row, col];
                if (ch == 'X' || ch == 'O')
                    return 0;
                else
                {


                    board[row, col] = 'X';

                    var moveResult = Move(row, col, 1);
                    if (movesCount >= 5 && moveResult == 0)
                        Result = 0;

                    if (movesCount >= 2)
                        Result = moveResult;

                    movesCount++;

                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        private int Move(int row, int col, int player)
        {
            int val = player == 1 ? 1 : -1;
            rows[row] += val;
            cols[col] += val;
            if (row == col) diagonal1 += val;

            if (rows[row] == length || cols[col] == length || diagonal1 == length || diagonal2 == length)
            {
                return 1;
            }

            if (rows[row] == -length || cols[col] == -length || diagonal1 == -length || diagonal2 == -length)
            {
                return -1;
            }

            return 0;
        }

        public void Print()
        {
            Write("    ");
            for (int col = 0; col < 3; col++)
            {
                Write(col + " | ");
            }
            WriteLine();
            for (int row = 0; row < 3; row++)
            {
                Write(row + " | ");
                for (int col = 0; col < 3; col++)
                {
                    Write(board[row, col] + " | ");
                }
                WriteLine();
            }
        }

        public void PrintMoves()
        {

        }



    }

    public class MoveTrack
    {
        public int MoveId { get; set; }
        public string player { get; set; }
        public int RowVal { get; set; }
        public int ColVal { get; set; }
        public int CompMoveCount { get; set; }
        public int PlayerMoveCount { get; set; }
    }

}

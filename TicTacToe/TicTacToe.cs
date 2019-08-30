using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace tictactoe
{
    /// <summary>
    /// Simple TicTacToe Game class
    /// </summary>
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
        /// <summary>
        /// Game Result
        /// </summary>
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

            // Initialize board move
            BoardMove.player = 0;

        }
        /// <summary>
        /// Reset the game
        /// </summary>
        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '\0';
                }
                rows[i] = 0;
                cols[i] = 0;

            }
            Result = null;
            compMoveCount = 0;
            movesCount = 0;
            diagonal1 = 0;
            diagonal2 = 0;
            BoardMove.player = 0;
            BoardMove.row = 0;
            BoardMove.col = 0;
        }
        /// <summary>
        /// TODO: Implement smart move algorithm
        /// For now , select random moves
        /// </summary>
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

            BoardMove.player = -1;
            BoardMove.row = row;
            BoardMove.col = col;
            compMoveCount++;
        }
        /// <summary>
        /// Apply player Move
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int PlayerMove(string pos)
        {

            int row, col;
            int player = 1;
            char ch;
            if (pos.Length == 2 && int.TryParse(pos.Substring(0, 1), out row) && int.TryParse(pos.Substring(1, 1), out col) && row >= 0 && row <= 2 && col >= 0 && col <= 2)
            {
                ch = board[row, col];
                if (ch == 'X' || ch == 'O')
                    return 0;
                else
                {
                    board[row, col] = 'X';

                    var moveResult = Move(row, col, player);
                    if (movesCount >= 5 && moveResult == 0)
                        Result = 0;

                    if (movesCount >= 2)
                        Result = moveResult;

                    BoardMove.player = player;
                    BoardMove.row = row;
                    BoardMove.col = col;
                    movesCount++;

                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Construct the result of the move played either by player or computer with 1: Player Win, -1: COmputer Win , 0: Tie
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="player"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Print/Draw the current game state
        /// </summary>
        public void Print()
        {
            WriteLine();
            WriteLine("*** BOARD STATE ***");
            WriteLine("--------------------");
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
            WriteLine("--------------------");
            WriteLine();
            WriteLine($"Board State: {BoardMove.LastMoveMsg}");
            WriteLine();
        }

        

        public void PrintMoves()
        {

        }



    }
    /// <summary>
    /// Play moves log
    /// </summary>
    public class MoveTrack
    {
        public int MoveId { get; set; }
        public string player { get; set; }
        public int RowVal { get; set; }
        public int ColVal { get; set; }
        public int CompMoveCount { get; set; }
        public int PlayerMoveCount { get; set; }
    }

    public static class BoardMove
    {
        public static int player { get; set; }
        public static int row { get; set; }
        public static int col { get; set; }

        public static string LastMoveMsg { get {
                if (player == 0)
                    return "Initial. player to play first..";
                else
                {
                    var msg = (player == 1) ? "Player played:" : "Computer player:";
                    return $"{msg} Row: {row}, Col: {col}";
                }
            } }

    }

}

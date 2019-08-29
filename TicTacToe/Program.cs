using System;
using static System.Console;

namespace tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {

            TicTacToe game = new TicTacToe(3);
            // Initial board


            bool keepPlaying = true;

            while (keepPlaying)
            {
                Clear();
                game.Print();

                while (game.Result == null)
                {
                    Write("Play your position:");
                    string pos = ReadLine();

                    var move = game.PlayerMove(pos);

                    if (move == -1)
                    {
                        WriteLine($"You played {pos} out of range or invalid..pls try again");
                        continue;
                    }

                    if (move == 0)
                    {
                        WriteLine($"You played {pos} which is already played..pls try again");
                        continue;
                    }

                    if (game.Result == null)
                        game.computerMove();

                    Clear();
                    game.Print();

                }

                switch (game.Result)
                {
                    case 1:
                        WriteLine("Congrates, you WON!!");
                        break;
                    case 2:
                        WriteLine("Sorry computer won, try again");
                        break;
                    case 0:
                        WriteLine("Oops! Its Tie");
                        break;

                }

                WriteLine("Would you like to play again?");
                string ans = "";

                while (true)
                {
                    ans = ReadLine();
                    if (ans.Equals("Y") || ans.Equals("N"))
                        break;
                    else
                        WriteLine("Invalid input, pls try again...");
                }


                if (ans.Equals("N"))
                    keepPlaying = false;
                else
                    game.ResetBoard();

            }


        }
    }
}

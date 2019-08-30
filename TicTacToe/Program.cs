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

            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Tic Tac Toe"));
            // TODO: Move to readme.txt file
            WriteLine("Hi There! Greetings!!");
            WriteLine("This is very simple TicTacToe 3X3 board game between you as player and COmputer as another player");
            WriteLine("However, this does not make any smart computer moves..means more chances for you to win.");
            WriteLine("Hint: Computer makes a random Dumb moves");

            WriteLine();
            WriteLine("How to..");
            WriteLine("\tThis simple game will allow you to make first play");
            WriteLine("\tTo play your position, type matrix coordinate of 3x3 board e.g. for Row 0, Col 0 , enter 00 OR To play Row 2, Col 1 -> Enter 21 with no quotes and so on");
            WriteLine();
            WriteLine("Caution: This is initial draft version and not fully tested for all moves and may contain issues/Bugs");
            WriteLine("Coming soon in next version...smart Computer move algorithm");
            WriteLine("Enjoy!!!");

            bool keepPlaying = true;
            bool gameStarted = false;

            while (keepPlaying)
            {
                if (gameStarted)
                {
                    Clear();
                }

                gameStarted = true;

                game.Print();

                while (game.Result == null)
                {
                    WriteLine(@"Play your position: (e.g. for Row 0, Col 0 , enter 00 OR To play Row 2, Col 1 -> type 21 (with no quotes) and Hit ENTER and so on)");
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
                    case -1:
                        WriteLine("Sorry computer won, try again");
                        break;
                    case 0:
                        WriteLine("Oops! Its Tie");
                        break;

                }

                WriteLine("Would you like to play again? (Y/N)");
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

using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using System;
using System.Threading;

namespace BattleShip.UI
{
    public static class ConsoleIO
    {
        internal static void SplashScreen(string message)
        { 
            Console.Clear();
            WriteInColor("                          ****************\n", ConsoleColor.DarkCyan);
            WriteInColor("                          ***", ConsoleColor.DarkCyan);
            WriteInColor("BattleShip", ConsoleColor.Green);
            WriteInColor("***\n", ConsoleColor.DarkCyan);
            WriteInColor("                          ****************\n\n\n", ConsoleColor.DarkCyan);
            WriteInColor($"                  {message}", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        internal static void PrintBoard(Game game, string message)
        {
            const string separator = "          +---+---+---+---+---+---+---+---+---+---+---+";
            string[] rowLetters = { "|A  ", "|B  ", "|C  ", "|D  ", "|E  ", "|F  ", "|G  ", "|H  ", "|I  ", "|J  " };
            string cellChar = " ";

            Player activePlayer;
            Player enemyPlayer;
            if (game.Player1Turn)
            {
                activePlayer = game.Player1;
                enemyPlayer = game.Player2;

            }
            else
            {
                activePlayer = game.Player2;
                enemyPlayer = game.Player1;
            }

            Console.Clear();
            Console.Write("          ");
            WriteInColor(activePlayer.Name, ConsoleColor.Green);
            Console.WriteLine($", {message}.\n\n");
            WriteInColor(separator + "\n", ConsoleColor.White);
            WriteInColor("          |   | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |10 |\n", ConsoleColor.White);
            WriteInColor(separator + "\n", ConsoleColor.White);
            for (int row = 1; row < 11; row++)
            {
                Console.Write("          ");
                WriteInColor(rowLetters[row - 1], ConsoleColor.White);
                for (int col = 1; col < 11; col++)
                {
                    Coordinate currentCoord = new Coordinate(row, col);
                    cellChar = GetCellChar(enemyPlayer.PlayerBoard, currentCoord);
                    WriteInColor("| ", ConsoleColor.White);
                    WriteInColor(cellChar, GetCellColor(cellChar));
                    Console.Write(" ");
                }
                WriteInColor("|", ConsoleColor.White);
                WriteInColor("\n" + separator + "\n", ConsoleColor.White);
            }
        }

        private static ConsoleColor GetCellColor(string cellChar)
        {
            ConsoleColor color = ConsoleColor.Cyan;
            switch (cellChar)
            {
                case "H":
                    color = ConsoleColor.Red;
                    break;

                case "M":
                    color = ConsoleColor.Yellow;
                    break;

                case "*":
                    color = ConsoleColor.Blue;
                    break;

                default:
                    throw new Exception("What have you done? (Contact IT)");
            }
            return color;
        }

        private static string GetCellChar(Board board, Coordinate coord)
        {
            string cellChar = " ";
            ShotHistory cellState = board.CheckCoordinate(coord);

            switch (cellState)
            {
                case ShotHistory.Hit:
                    cellChar = "H";
                    break;

                case ShotHistory.Miss:
                    cellChar = "M";
                    break;

                case ShotHistory.Unknown:
                    cellChar = "*";
                    break;

                default:
                    throw new Exception("What have you done? (Contact IT)");
            }
            return cellChar;
        }

        internal static void WriteInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = Settings.TextColor;
        }

        internal static void CoinFlipDisplay(Player player1, Player player2)
        {
            Console.Clear();
            Console.Write($"        {player1.Name}, press any key to flip a coin and determine who goes first.\n               (Heads: {player1.Name} goes first, Tails: {player2.Name} goes first.)");
            Console.ReadKey();
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n\n\n                                       _");
            Thread.Sleep(500);
            Console.Clear();
            Console.Write("\n\n                                       /");
            Thread.Sleep(60);
            Console.Clear();
            Console.Write("\n                                       _");
            Thread.Sleep(60);
            Console.Clear();
            for (int i = 0; i <= 2; i++)
            {
                Console.Write("                                       -");
                Thread.Sleep(50);
                Console.Clear();
                Console.Write(@"                                       \");
                Thread.Sleep(50);
                Console.Clear();
                Console.Write("                                       |");
                Thread.Sleep(50);
                Console.Clear();
                Console.Write("                                       /");
                Thread.Sleep(50);
                Console.Clear();

            }
            Console.Write("\n                                       _");
            Thread.Sleep(60);
            Console.Clear();
            Console.Write("\n\n                                       /");
            Thread.Sleep(60);
            Console.Clear();
            Console.WriteLine("\n\n\n                                       _\n\n\n");
            Thread.Sleep(700);
            Console.CursorVisible = true;
            Console.ForegroundColor = Settings.TextColor;
        }

        internal static string GetUserInput()
        {
            Console.ForegroundColor = Settings.InputTextColor;
            string input = Console.ReadLine();
            Console.ForegroundColor = Settings.TextColor;
            return input;
        }
    }
}




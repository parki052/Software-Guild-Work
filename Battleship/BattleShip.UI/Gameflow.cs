using BattleShip.BLL.Responses;
using System;


namespace BattleShip.UI
{
    public class Gameflow
    {
        internal static void Run()
        {
            bool playAgain = true;
            while (playAgain)
            {
                ConsoleIO.SplashScreen("Press any key to start a new game.");

                Game game = SetupWorkflow.Run();
                AlternateFiring(game);

                playAgain = PlayAnotherGame();
            }
            ConsoleIO.SplashScreen("        Thanks for playing!");       
        }

        private static void GetPlayerShot(Game game)
        {
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
            FireShotResponse shot = new FireShotResponse() { ShotStatus = ShotStatus.Invalid };

            while (shot.ShotStatus == ShotStatus.Invalid || shot.ShotStatus == ShotStatus.Duplicate)
            {
                shot = enemyPlayer.PlayerBoard.FireShot(CoordinateWorkflow.GetCoordinate(game, "take your shot"));

                ReportShot(shot, activePlayer, enemyPlayer);
                if (shot.ShotStatus == ShotStatus.Victory)
                {
                    game.GameOver = true;
                }
            }
        }

        private static void ReportShot(FireShotResponse shot, Player activePlayer, Player enemyPlayer)
        {
            switch (shot.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    Console.Clear();
                    Console.Write($"Error, you can't fire at the same spot more than once. Press any key to continue...");
                    Console.ReadKey();
                    break;

                case ShotStatus.Hit:
                    Console.Clear();
                    ConsoleIO.WriteInColor("Hit! ", ConsoleColor.Red);
                    Console.Write($"You hit {enemyPlayer.Name}'s {shot.ShipImpacted}. Press any key to continue...");
                    Console.ReadKey();
                    break;

                case ShotStatus.HitAndSunk:
                    Console.Clear();
                    ConsoleIO.WriteInColor("BOOM! ", ConsoleColor.Red);
                    Console.Write($"You sunk {enemyPlayer.Name}'s {shot.ShipImpacted}! Press any key to continue...");
                    Console.ReadKey();
                    break;

                case ShotStatus.Invalid:
                    throw new Exception("What have you done? (Contact IT)");

                case ShotStatus.Miss:
                    Console.Clear();
                    Console.Write($"You missed. Press any key to continue...");
                    Console.ReadKey();
                    break;

                case ShotStatus.Victory:
                    Console.Clear();
                    ConsoleIO.WriteInColor("    BOOM! ", ConsoleColor.Red);
                    Console.WriteLine($"With a final blow, {activePlayer.Name} sunk {enemyPlayer.Name}'s {shot.ShipImpacted}.\n              {enemyPlayer.Name}'s fleet has been destroyed!");
                    Console.Write($"         {activePlayer.Name} has won! Better luck next time, {enemyPlayer.Name}\n\n                      Game over.");
                    break;

                default:
                    Console.WriteLine("What have you done? (Contact IT)");
                    break;
            }
        }

        private static void AlternateFiring(Game game)
        {
            while (!game.GameOver)
            {
                if (game.Player1Turn)
                {
                    Player activePlayer = game.Player1;
                    Player enemyPlayer = game.Player2;
                    GetPlayerShot(game);
                    game.Player1Turn = !game.Player1Turn;
                }
                else
                {
                    Player activePlayer = game.Player2;
                    Player enemyPlayer = game.Player1;
                    GetPlayerShot(game);
                    game.Player1Turn = !game.Player1Turn;
                }
            }
        }

        private static bool PlayAnotherGame()
        {
            bool playAgain = false;

            Console.Write("\n\n       Press any key to play again, or press q to quit: ");
            ConsoleKeyInfo cki = Console.ReadKey();

            if(cki.Key != ConsoleKey.Q)
            {
                playAgain = true;
            }

            return playAgain;
        }
    }
}

using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;

namespace BattleShip.UI
{
    public class SetupWorkflow
    {
        
        public static Game Run()
        {
            Game game = new Game();

            SetPlayerNames(game);

            game.Player1Turn = DetermineStartingPlayer(game);

            SetPlayerOrder(game);

            SetPlayerBoards(game);

            return game;
        }

        internal static string GetPlayerName(Player player)
        {
            Console.Clear();

            Console.Write($"Player {player.Index}, please enter your name: ");
            return ConsoleIO.GetUserInput();
        }

        private static bool DetermineStartingPlayer(Game game)
        {
            Player player1 = game.Player1;
            Player player2 = game.Player2;

            ConsoleIO.CoinFlipDisplay(player1, player2);

            bool doesPlayerOneStart = false;
            int rand = RNG.GetRandomNumber(1, 2);

            if (rand == 1)
            {
                doesPlayerOneStart = true;
                Console.WriteLine($"                            Heads! {player1.Name} goes first!");
            }
            else
            {
                doesPlayerOneStart = false;
                Console.WriteLine($"                            Tails! {player2.Name} goes first!");
            }

            Console.Write("                          Press any key to continue...");
            Console.ReadKey();
            return doesPlayerOneStart;
        }

        private static void SetPlayerOrder(Game game)
        {
            Player player1;
            Player player2;
            if (game.Player1Turn == true)
            {
                player1 = game.Player1;
                player2 = game.Player2;
            }
            else
            {
                player1 = game.Player2;
                player2 = game.Player2;
            }
        }

        private static void SetPlayerNames(Game game)
        {
            game.Player1.Name = GetPlayerName(game.Player1);
            game.Player2.Name = GetPlayerName(game.Player2);         
        }

        private static void SetPlayerBoards(Game game)
        {
                SetShips(game, game.Player1);
                game.Player1Turn = !game.Player1Turn;

                SetShips(game, game.Player2);
                game.Player1Turn = !game.Player1Turn;
        }

        private static void SetShips(Game game, Player player)
        {
            if (game.Player1Turn)
            {
                player = game.Player1;

            }
            else
            {
                player = game.Player2;
            }

            foreach (ShipType ship in Enum.GetValues(typeof(ShipType)))
            {
                ShipPlacement result = ShipPlacement.NotEnoughSpace;
                while (result != ShipPlacement.Ok)
                {
                    PlaceShipRequest request = new PlaceShipRequest()
                    {
                        ShipType = ship,
                        Coordinate = CoordinateWorkflow.GetCoordinate(game, $"time to place your {ship}"),
                        Direction = GetDirectionChoice(player, ship)
                    };

                    result = player.PlayerBoard.PlaceShip(request);
                    ShipPlacementResponse(result, ship);
                }
            }
            Console.Clear();
            ConsoleIO.WriteInColor(player.Name, ConsoleColor.Green);
            Console.Write(", all ships placed successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private static void ShipPlacementResponse(ShipPlacement result, ShipType ship)
        {
            switch (result)
            {
                case ShipPlacement.Ok:
                    Console.Write($"          {ship} placed successfully! Press any key to continue...");
                    Console.ReadKey();
                    break;

                case ShipPlacement.NotEnoughSpace:
                    Console.Write($"          Error, you tried to place {ship} off the board. Press any key to try again...");
                    Console.ReadKey();
                    break;

                case ShipPlacement.Overlap:
                    Console.Write($"          Error, you tried to place {ship} overlapping another ship. Press any key to try again...");
                    Console.ReadKey();
                    break;

                default:

                    Console.Write("What have you done? (Contact IT)");
                    Console.ReadKey();
                    break;
            }
        }

        private static ShipDirection GetDirectionChoice(Player player, ShipType ship)
        {
            bool validInput = false;
            ShipDirection direction = ShipDirection.Down;

            while (!validInput)
            {
                Console.Write($"          {player.Name}, please enter a direction for your {ship}: ");
                string userInput = ConsoleIO.GetUserInput().ToUpper();

                switch (userInput)
                {
                    case "U":
                        direction = ShipDirection.Up;
                        validInput = true;
                        break;

                    case "D":
                        direction = ShipDirection.Down;
                        validInput = true;
                        break;

                    case "L":
                        direction = ShipDirection.Left;
                        validInput = true;
                        break;

                    case "R":
                        direction = ShipDirection.Right;
                        validInput = true;
                        break;

                    default:
                        Console.WriteLine("          Error: invalid direction. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
            return direction;
        }
    }
}

using BattleShip.BLL.Requests;
using System;

namespace BattleShip.UI
{
    public static class CoordinateWorkflow
    {
        internal static Coordinate GetCoordinate(Game game, string message)
        {
            bool validCoord = false;
            string userInput = "";
            while (!validCoord)
            {
                ConsoleIO.PrintBoard(game, message);
                userInput = GetUserCoordInput();
                validCoord = ValidateCoordinate(userInput);

                if (!validCoord)
                {
                    Console.WriteLine("          Error: Invalid coordinate. Press any key to retry...");
                    Console.ReadKey();
                }
            }

            return CreateCoordinate(userInput);
        }

        public static Coordinate CreateCoordinate(string userInput)
        {
            Coordinate coordinate = new Coordinate(
                ConvertCharToInt(userInput.Substring(0, 1)),
                int.Parse(userInput.Substring(1, userInput.Length - 1))
                );

            return coordinate;
        }

        private static string GetUserCoordInput()
        {
            Console.Write($"\n          Please enter a coordinate. (ex. A1 - J10): ");
            return ConsoleIO.GetUserInput();
        }

        public static int ConvertCharToInt(string letter)
        {
            return char.Parse(letter) % 32;
        }

        public static bool ValidateCoordinate(string userInput)
        {
            bool validCoord = false;

            if (userInput.Length >= 2 && userInput.Length <= 3)
            {
                string colString = userInput.Substring(0, 1);
                string rowString = userInput.Substring(1, userInput.Length - 1);

                if (IsInValidRange(ConvertCharToInt(colString).ToString()) && IsInValidRange(rowString))
                {
                    validCoord = true;
                }
            }

            return validCoord;
        }

        public static bool IsInValidRange(string numberString)
        {
            bool isValidRange = false;

            if (int.TryParse(numberString, out int number))
            {
                if (number >= 1 && number <= 10)
                {
                    isValidRange = true;
                }
            }
            return isValidRange;
        }
    }
}

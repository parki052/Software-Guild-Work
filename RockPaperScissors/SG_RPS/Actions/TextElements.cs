using SG_RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Actions
{
    static class TextElements
    {

        public static void DisplaySplash()
        {
            Console.Clear();
            Console.WriteLine("Rock Paper Scissors\n");
            Console.WriteLine("*********************\n");
            Console.WriteLine("Press any key to begin.");
            Console.ReadKey();
        }

        public static void DisplayRoundResult(MatchResult result, GameState state)
        {
            Console.Clear();
            Console.WriteLine($"(Round {state.CurrentRound}/{state.TotalRounds})\n");
            Console.WriteLine($"{state.PlayerName} picked {result.PlayerChoice}. Computer picked {result.ComputerChoice}.\n");
            switch(result.RoundWinner)
            {
                case RoundWinner.Computer:
                    Console.Write($"Computer beat {state.PlayerName}!");
                    break;

                case RoundWinner.Player:
                    Console.Write($"{state.PlayerName} beat computer!");
                    break;

                case RoundWinner.Tie:
                    Console.Write($"{state.PlayerName} and the computer tied!");
                    break;
            }
            Console.Write(" Press any key to continue...");
            Console.ReadKey();

        }

        public static void DisplayFinalResult(GameManager game)
        {
            Console.Clear();

            const string format = "{0, -11} {1, -22} {2, -22} {3, -22}";
            const string separator = "******************************************************************************";
            string printString;

            int playerWins = 0;
            int computerWins = 0;
            int ties = 0;

            printString = string.Format(format, "Round #", $"{game.GameState.PlayerName}'s Choice", "Computer's Choice", "Winner");
            Console.WriteLine("Results:\n\n");
            Console.WriteLine(printString);
            Console.WriteLine();
            Console.WriteLine(separator);

            foreach (MatchResult result in game.GameHistory.AllRoundHistory)
            {
                printString = string.Format(format, $"Round #{result.RoundNumber}", result.PlayerChoice, result.ComputerChoice, result.RoundWinner);
                Console.WriteLine(printString);
                Console.WriteLine(separator);
                switch(result.RoundWinner)
                {
                    case RoundWinner.Computer:
                        computerWins++;
                        break;
                    case RoundWinner.Player:
                        playerWins++;
                        break;
                    case RoundWinner.Tie:
                        ties++;
                        break;
                }
            }

            Console.WriteLine($"\n{game.GameState.PlayerName} wins: {playerWins}\nComputer wins: {computerWins}\nTies: {ties}\n");

            if (computerWins > playerWins)
            {
                Console.WriteLine("Computer wins! Better luck next time.");
            }
            else if (playerWins > computerWins)
            {
                Console.WriteLine($"{game.GameState.PlayerName} wins! Good job.");
            }
            else
            {
                Console.WriteLine("Tie game!");
            }
        }
    }
}

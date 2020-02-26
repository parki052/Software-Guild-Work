using SG_RPS.Models;
using System;

namespace SG_RPS.Actions
{
    public class GameFlow
    {

        public static void Run()
        {
            TextElements.DisplaySplash();

            GameState state = new GameState(UserInput.GetUserName(), UserInput.GetNumberOfRounds());
            GameHistory gameHistory = new GameHistory();
            GameManager game = new GameManager(gameHistory, state);


            for (int i = 0; i < game.GameState.TotalRounds; i++)
            {
                gameHistory.AllRoundHistory.Add(RunRound(game));
            }

            TextElements.DisplayFinalResult(game);
        }

        public static MatchResult RunRound(GameManager game)
        {
            Console.Clear();
            game.GameState.CurrentRound += 1;

            Console.WriteLine($"(Round {game.GameState.CurrentRound}/{game.GameState.TotalRounds})\n{game.GameState.PlayerName}, it's your turn! Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            MatchResult matchResult = new MatchResult
            {
                RoundNumber = game.GameState.CurrentRound,
                PlayerChoice = UserInput.GetUserPick(),
                ComputerChoice = GetComputerChoice()
            };
            matchResult.RoundWinner = DetermineWinner(matchResult.PlayerChoice, matchResult.ComputerChoice);

            TextElements.DisplayRoundResult(matchResult, game.GameState);
            return matchResult;
        }

        public static RoundWinner DetermineWinner(RoundChoice playerChoice, RoundChoice computerChoice)
        {
            RoundWinner winner = RoundWinner.Tie;

            switch (playerChoice)
            {
                case RoundChoice.Rock:

                    switch (computerChoice)
                    {
                        case RoundChoice.Rock:
                            winner = RoundWinner.Tie;
                            break;

                        case RoundChoice.Paper:
                            winner = RoundWinner.Computer;
                            break;

                        case RoundChoice.Scissors:
                            winner = RoundWinner.Player;
                            break;
                    }
                    break;

                case RoundChoice.Paper:

                    switch (computerChoice)
                    {
                        case RoundChoice.Rock:
                            winner = RoundWinner.Player;
                            break;

                        case RoundChoice.Paper:
                            winner = RoundWinner.Tie;
                            break;

                        case RoundChoice.Scissors:
                            winner = RoundWinner.Computer;
                            break;
                    }
                    break;

                case RoundChoice.Scissors:

                    switch (computerChoice)
                    {
                        case RoundChoice.Rock:
                            winner = RoundWinner.Computer;
                            break;

                        case RoundChoice.Paper:
                            winner = RoundWinner.Player;
                            break;

                        case RoundChoice.Scissors:
                            winner = RoundWinner.Tie;
                            break;
                    }
                    break;
            }
            return winner;
        }


        public static RoundChoice GetComputerChoice()
        {

            int randomNumber = RNG.GetRandomNumber(1, 3);
            RoundChoice computerChoice = RoundChoice.Paper;

            switch (randomNumber)
            {
                case 1:
                    computerChoice = RoundChoice.Rock;
                    break;
                case 2:
                    computerChoice = RoundChoice.Paper;
                    break;
                case 3:
                    computerChoice = RoundChoice.Scissors;
                    break;
            }

            return computerChoice;
        }
    }
}

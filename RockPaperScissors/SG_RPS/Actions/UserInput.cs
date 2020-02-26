using SG_RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_RPS.Actions
{
    public static class UserInput
    {

        public static string GetUserName()
        {
            Console.Clear();
            Console.Write("Please enter your name: ");

            string userName = Console.ReadLine();
            return userName;
        }

        public static int GetNumberOfRounds()
        {
            int numberOfRounds = 0;
            bool validInput = false;

            while(!validInput)
            {
                Console.Clear();
                Console.Write("How many rounds would you like to play?\n(Please enter a number 1-10): ");
                validInput = int.TryParse(Console.ReadLine(), out numberOfRounds);
                if(numberOfRounds < 1 || numberOfRounds > 10)
                {
                    validInput = false;
                }
                if(!validInput)
                {
                    Console.WriteLine("Error: Invalid input. Press any key to retry...");
                    Console.ReadKey();
                }                        
            }
            return numberOfRounds;
        }

        public static RoundChoice GetUserPick()
        {
            string userInput;
            bool validInput = false;
            RoundChoice userChoice = RoundChoice.Paper;

            while(!validInput)
            {
                Console.Clear();
                Console.Write("Please pick R for rock, P for paper, or S for scissors: ");
                userInput = Console.ReadLine().ToUpper();

                switch(userInput)
                {
                    case "R":
                        validInput = true;
                        userChoice = RoundChoice.Rock;
                        break;

                    case "P":
                        validInput = true;
                        userChoice = RoundChoice.Paper;
                        break;

                    case "S":
                        validInput = true;
                        userChoice = RoundChoice.Scissors;
                        break;

                    default:
                        Console.Write("Error: invalid input. Press any key to retry...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            return userChoice;
        }
    }
}

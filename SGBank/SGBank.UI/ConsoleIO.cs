using SGBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public class ConsoleIO
    {
        public static void DisplayAccountDetails(Account account)
        {
            Console.WriteLine($"\nAccount number: {account.AccountNumber}");
            Console.WriteLine($"Name: {account.Name}");
            Console.WriteLine($"Balance: {account.Balance:c}");
        }

        public static string GetAcctNumber() => Console.ReadLine();


        public static decimal GetDecimalAmount(string message)
        {
            bool validInput = false;
            decimal amount = decimal.MaxValue;
            while(!validInput)
            {
                Console.Write(message);
                validInput = decimal.TryParse(Console.ReadLine(), out amount);
                if(!validInput)
                {
                    Console.WriteLine("That was not a valid amount. Press any key to try again... ");
                    Console.ReadKey();
                }
            }
            return amount;
        }
    }
}

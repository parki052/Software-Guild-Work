using SGBank.BLL;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class AccountWithdrawWorkflow
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager manager = AccountManagerFactory.Create();

            Console.Write("Please enter an account number: ");
            string accountNumber = ConsoleIO.GetAcctNumber();

            decimal amount = ConsoleIO.GetDecimalAmount("Please enter an amount to withdraw (must be negative): ");

            AccountWithdrawResponse response = manager.Withdraw(accountNumber, amount);

            if(response.Success)
            {
                Console.Clear();
                Console.WriteLine("Withdraw successful!");
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Account number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount withdrawn: {response.Amount:c}");
                Console.WriteLine($"New balance: {response.Account.Balance:c}");
            }
            else
            {
                Console.WriteLine(response.Message);
            }
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}

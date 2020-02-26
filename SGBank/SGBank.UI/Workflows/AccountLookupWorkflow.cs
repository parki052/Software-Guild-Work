using SGBank.BLL;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class AccountLookupWorkflow
    {
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Lookup an account");
            Console.WriteLine("----------------------------");
            Console.Write("Enter an account number: ");
            string accountNumber = ConsoleIO.GetAcctNumber();

            AccountLookupResponse response = manager.LookupAccount(accountNumber);

            if(response.Success)
            {
                ConsoleIO.DisplayAccountDetails(response.Account);
            }
            else
            {
                Console.WriteLine(response.Message);
            }

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

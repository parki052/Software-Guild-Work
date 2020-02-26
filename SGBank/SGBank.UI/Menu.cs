using SGBank.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public class Menu
    {
        public static void Start()
        {
            bool userExit = false;

            while(!userExit)
            { 
                
            Console.Clear();
                Console.WriteLine("SG Bank Application");
                Console.WriteLine("----------------------------");
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("\nQ to quit");
                Console.Write("\nEnter selection: ");

                string userInput = Console.ReadLine();

                switch(userInput.ToUpper())
                {
                    case "1":
                        AccountLookupWorkflow lookupWorkflow = new AccountLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;

                    case "2":
                        AccountDepositWorkflow depositWorkflow = new AccountDepositWorkflow();
                        depositWorkflow.Execute();
                        break;

                    case "3":
                        AccountWithdrawWorkflow withdrawWorkflow = new AccountWithdrawWorkflow();
                        withdrawWorkflow.Execute();
                        break;

                    case "Q":
                        userExit = true;
                        break;
                }
            }
        }
    }
}

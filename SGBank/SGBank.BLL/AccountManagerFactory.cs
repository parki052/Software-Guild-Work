using Ninject;
using SGBank.Data;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL
{
    public static class AccountManagerFactory
    {
        public const string livePath = @"C:\Data\SGBank\Accounts.txt";
        public static AccountManager Create()
        {

            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "FreeTest":
                    return new AccountManager(new FreeAccountTestRepository());
                case "BasicTest":
                    return new AccountManager(new BasicAccountTestRepository());
                case "PremiumTest":
                    return new AccountManager(new PremiumAccountTestRepository());
                case "LiveRepo":
                    return new AccountManager(new FileAccountRepository(livePath));

                default:
                    throw new Exception("Mode value in app config is not valid.");
            }



        }
    }
}

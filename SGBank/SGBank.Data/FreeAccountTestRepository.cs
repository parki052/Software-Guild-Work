using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FreeAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Free Account",
            Balance = 100.00m,
            AccountNumber = "12345",
            Type = AccountType.Free

        };
        public Account LoadAccount(string AccountNumber)
        {
            Account account = null;
            if (AccountNumber == _account.AccountNumber)
            {
                account = _account;
            }

            return account;
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}

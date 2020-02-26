using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class BasicAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Basic Account",
            Balance = 100M,
            AccountNumber = "33333",
            Type = AccountType.Basic
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

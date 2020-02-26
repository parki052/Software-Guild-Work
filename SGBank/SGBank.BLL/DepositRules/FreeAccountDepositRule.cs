using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.DepositRules
{
    public class FreeAccountDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            if (account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: A non-free account hit the free deposit rule. Contact IT.";
            }
            else if (amount > 100)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: Free accounts cannot deposit more than $100 at a time.";
            }
            else if (amount <= 0)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: Deposit amounts must be greater than zero.";
            }
            else
            {
                response.OldBalance = account.Balance;
                account.Balance += amount;
                response.Account = account;
                response.Amount = amount;
                response.Success = true;
            }
            return response;

        }
    }
}

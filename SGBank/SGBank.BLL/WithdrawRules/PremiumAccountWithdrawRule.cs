using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: a non-premium account hit the premium withdraw rule. Contact IT.";
                return response;
            }
            if (amount >= 0)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: withdrawal amounts must be negative.";
                return response;
            }
            if (account.Balance + amount <= -500)
            {
                response.Success = false;
                response.Account = account;
                response.Message = $"Error: premium accounts cannot go below -500$. (Current balance: {account.Balance:c})";
                return response;
            }

            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            account.Balance += amount;


            return response;
        }
    }
}

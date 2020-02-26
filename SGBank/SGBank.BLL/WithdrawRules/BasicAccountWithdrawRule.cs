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
    public class BasicAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if(account.Type != AccountType.Basic)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: a non-basic account hit the basic withdraw rule. Contact IT.";
                return response;
            }
            if(amount >= 0)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: withdrawal amounts must be negative.";
                return response;
            }
            if(amount < -500)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: basic accounts cannot withdraw more than $500 at once.";
                return response;
            }
            if(amount + account.Balance < -100)
            {
                response.Success = false;
                response.Account = account;
                response.Message = "Error: basic account cannot overdraft more than $100.";
                return response;
            }

            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalance = account.Balance;
            account.Balance += amount;
            if(account.Balance < 0)
            {
                account.Balance -= 10;
            }

            return response;
        }
    }
}

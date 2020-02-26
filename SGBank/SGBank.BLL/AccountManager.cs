using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL
{
    public class AccountManager
    {
        private IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountLookupResponse LookupAccount(string accountNumber)
        {
            AccountLookupResponse response = new AccountLookupResponse();
            try
            {
                response.Account = _accountRepository.LoadAccount(accountNumber);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            if (response.Account == null)
            {
                response.Success = false;
                response.Message = $"{accountNumber} is not a valid account.";
                return response;
            }

            response.Success = true;
            return response;
        }

        public AccountDepositResponse Deposit(string accountNumber, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            try
            {
                response.Account = _accountRepository.LoadAccount(accountNumber);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            if (response.Account == null)
            {
                response.Success = false;
                response.Message = $"{accountNumber} is not a valid account.";
                return response;
            }
            else if (response.Account.Name == "Error")
            {
                response.Success = false;
                response.Message = "Error: something went wrong while accessing the file repository. Contact IT.";
                return response;
            }
            else
            {
                response.Success = true;
            }

            IDeposit depositRule = DepositRulesFactory.CreateDepositRule(response.Account.Type);
            response = depositRule.Deposit(response.Account, amount);
            if (response.Success)
            {
                try
                {
                    _accountRepository.SaveAccount(response.Account);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                    return response;
                }
            }

            return response;
        }

        public AccountWithdrawResponse Withdraw(string accountNumber, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            try
            {
                response.Account = _accountRepository.LoadAccount(accountNumber);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            if (response.Account == null)
            {
                response.Success = false;
                response.Message = $"Error: {accountNumber} is not a valid account number. ";
                return response;
            }


            IWithdraw withdraw = WithdrawRulesFactory.Create(response.Account.Type);
            response = withdraw.Withdraw(response.Account, amount);

            if (response.Success)
            {
                try
                {
                    _accountRepository.SaveAccount(response.Account);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }

            }
            return response;
        }
    }
}

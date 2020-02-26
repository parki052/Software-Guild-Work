using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class PremiumAccountTests
    {

        [TestCase("55555", "Premium Account", 100, AccountType.Free, 250, false, 100)] //fail, wrong account type
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, -100, false, 100)] //fail, negative number deposited
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, 250, true, 350)] //pass
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult, decimal expectedBalance)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account
            {
                AccountNumber = accountNumber,
                Balance = balance,
                Name = name,
                Type = accountType
            };

            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
            Assert.AreEqual(expectedBalance, response.Account.Balance);
        }

        [TestCase("55555", "Premium Account", 1500, AccountType.Premium, -1000, 500, true)] //pass
        [TestCase("55555", "Premium Account", 100, AccountType.Free, -100, 100, false)] //fail, wrong acct type
        [TestCase("55555", "Premium Account", 100, AccountType.Premium, 100, 100, false)] //fail, positive number withdrawn
        [TestCase("55555", "Premium Account", 0, AccountType.Premium, -501, 0, false)] //false, can't go under -500
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            Account account = new Account
            {
                AccountNumber = accountNumber,
                Balance = balance,
                Name = name,
                Type = accountType
            };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            Assert.AreEqual(newBalance, response.Account.Balance);
        }
    }
}

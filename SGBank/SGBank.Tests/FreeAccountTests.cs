using NUnit.Framework;
using SGBank.BLL;
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
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }


        [TestCase ("12345", "Free Account", 100, AccountType.Free, 250, false, 100)] //fail, too much deposited
        [TestCase ("12345", "Free Account", 100, AccountType.Free, -100, false, 100)] //fail, negative number deposited
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, false, 100)] //fail, not a free account
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, true, 150)] //pass
        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult, decimal expectedBalance)
        {
            IDeposit deposit = new FreeAccountDepositRule();
            Account account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };

            AccountDepositResponse response = deposit.Deposit(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
            Assert.AreEqual(expectedBalance, response.Account.Balance);

        }

        [TestCase("12345", "Free Account", 50, AccountType.Free, -51, false, 50)] //fail, cannot overdraft
        [TestCase("12345", "Free Account", 150, AccountType.Free, -101, false, 150)] //fail, cannot withdraw over 100 at once
        [TestCase("12345", "Free Account", 100, AccountType.Free, 100, false, 100)] //fail, positive number withdrawn
        [TestCase("12345", "Free Account", 100, AccountType.Basic, -50, false, 100)] //fail, not a free account
        [TestCase("12345", "Free Account", 100, AccountType.Free, -50, true, 50)] //pass
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult, decimal expectedBalance)
        {
            IWithdraw withdraw = new FreeAccountWithdrawRule();
            Account account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
            Assert.AreEqual(expectedBalance, response.Account.Balance);

        }
    }
}

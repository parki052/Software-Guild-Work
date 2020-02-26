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
    public class BasicAccountTests
    {
        [TestCase ("33333", "Basic Account", 100, AccountType.Free, 250, false, 100)] //fail, wrong account type
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, -100, false, 100)] //fail, negative number deposited
        [TestCase("33333", "Basic Account", 100, AccountType.Basic, 250, true, 350)] //pass
        public void BasicAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult, decimal expectedBalance)
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

        [TestCase ("33333", "Basic Account", 1500, AccountType.Basic, -1000, 1500, false)] //fail, too much withdrawn
        [TestCase ("33333", "Basic Account", 100, AccountType.Free, -100, 100, false)] //fail, wrong acct type
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, 100, 100, false)] //fail, positive number withdrawn
        [TestCase ("33333", "Basic Account", 100, AccountType.Basic, -150, -60, true)] //pass, overdraft fee deducted
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new BasicAccountWithdrawRule();
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

using NUnit.Framework;
using SGBank.Data;
using SGBank.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{

    [TestFixture]
    public class FileAccountTests
    {
        private const string _seedPath = @"C:\Data\SGBank\AccountTestSeed.txt";
        private const string _testDataPath = @"C:\Data\SGBank\AccountTest.txt";

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(_testDataPath))
            {
                File.Delete(_testDataPath);
            }

            File.Copy(_seedPath, _testDataPath);
        }

        [Test]
        public void CanReadAccountsFromFile()
        {
            FileAccountRepository repo = new FileAccountRepository(_testDataPath);
            List<Account> accounts = repo.GetAccountsFromFile(_testDataPath);

            Assert.AreEqual(4, accounts.Count); // there should be 4 accounts in the repo
        }

        [Test]
        public void CanEditFileAccount()
        {
            FileAccountRepository repo = new FileAccountRepository(_testDataPath);

            Account updatedAcct = new Account
            {
                AccountNumber = "11111",
                Name = "Free Customer",
                Balance = 1000m,
                Type = AccountType.Free
            };

            repo.SaveAccount(updatedAcct);

            List<Account> updatedAccounts = repo.GetAccountsFromFile(_testDataPath);

            Account modifiedAcct = updatedAccounts.Single(a => a.AccountNumber == "11111");

            Assert.AreEqual(updatedAcct.Balance, modifiedAcct.Balance); //balance should be raised to 1000m
        }

        [TestCase ("F", AccountType.Free)]
        [TestCase("B", AccountType.Basic)]
        [TestCase("P", AccountType.Premium)]
        public void CanParseLetterToAcctType(string letter, AccountType expected)
        {
            FileAccountRepository repo = new FileAccountRepository("");
            Assert.AreEqual(expected, repo.ParseLetterToAccountType(letter));
        }

        [TestCase (AccountType.Free, "F")]
        [TestCase(AccountType.Basic, "B")]
        [TestCase(AccountType.Premium, "P")]
        public void CanParseAcctTypeToLetter(AccountType type, string expected)
        {
            FileAccountRepository repo = new FileAccountRepository("");

            Assert.AreEqual(expected, repo.ParseAccountTypeToLetter(type));
        }
    }
}

using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {

        public string Path { get; set; }
        public FileAccountRepository(string path) => Path = path;


        public Account LoadAccount(string AccountNumber)
        {
            return GetAccountsFromFile(Path).SingleOrDefault(a => a.AccountNumber == AccountNumber);
        }

        public void SaveAccount(Account account) => UpdateAccountFile(account);


        public List<Account> GetAccountsFromFile(string path)
        {
            List<Account> accounts = new List<Account>();
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    if (line != null)
                    {
                        string[] row = line.Split(',');

                        Account account = new Account
                        {
                            AccountNumber = row[0],
                            Name = row[1],
                            Balance = Decimal.Parse(row[2]),
                            Type = ParseLetterToAccountType(row[3])
                        };
                        accounts.Add(account);
                    }
                }
            }
            return accounts;
        }

        public void UpdateAccountFile(Account account)
        {
            List<Account> accounts = GetAccountsFromFile(Path);

            accounts.Remove(accounts.Single(a => a.AccountNumber == account.AccountNumber));
            accounts.Add(account);

            using (StreamWriter sw = new StreamWriter(Path))
            {
                const string header = "AccountNumber,Name,Balance,Type";
                sw.WriteLine(header);

                foreach (var acct in accounts)
                {
                    sw.WriteLine(acct.AccountNumber + ',' + acct.Name + ',' + acct.Balance + ',' + ParseAccountTypeToLetter(acct.Type));
                }
            }
        }

        public AccountType ParseLetterToAccountType(string letter)
        {
            switch (letter.ToUpper())
            {

                case "F":
                    return AccountType.Free;

                case "B":
                    return AccountType.Basic;

                case "P":
                    return AccountType.Premium;

                default:
                    throw new Exception("Invalid account type in text repo. Contact IT.");
            }
        }

        public string ParseAccountTypeToLetter(AccountType type)
        {
            switch (type)
            {
                case AccountType.Basic:
                    return "B";

                case AccountType.Free:
                    return "F";

                case AccountType.Premium:
                    return "P";

                default:
                    throw new Exception("Error: the account type to save is unrecognized. Contact IT.");
            }

        }
    }
}

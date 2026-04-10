using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using banking.Data;
using banking.Models;

namespace banking.Business
{
    public class AccountService
    {
        private readonly AccountData accountData = new AccountData();
        private readonly TransactionData transactionData = new TransactionData();

        public List<Account> GetAllAccounts()
        {
            return accountData.GetAllAccounts();
        }

        public Account GetAccountByNumber(int accountNumber)
        {
            return accountData.GetAccountByNumber(accountNumber);
        }

        public void AddAccount(Account account)
        {
            if (account.AccountType == "Savings")
            {
                account.OverdraftLimit = 0;
            }

            if (account.AccountNumber.ToString().Length != 8)
            {
                throw new Exception("Account number must be 8 digits.");
            }

            accountData.AddAccount(account);
        }

        public void UpdateAccount(Account account)
        {
            accountData.UpdateAccount(account);
        }

        public void Deposit(int accountNumber, decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception("Deposit amount must be greater than zero.");
            }

            accountData.Deposit(accountNumber, amount);

            transactionData.AddTransaction(new TransactionRecord
            {
                SourceAccountNumber = accountNumber,
                DestinationAccountNumber = null,
                TransactionType = "Deposit",
                Amount = amount,
                ReferenceNumber = Guid.NewGuid().ToString(),
                TransactionDate = DateTime.Now
            });
        }

        public void Withdraw(int accountNumber, decimal amount)
        {
            Account account = accountData.GetAccountByNumber(accountNumber);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            if (amount <= 0)
            {
                throw new Exception("Withdrawal amount must be greater than zero.");
            }

            decimal limit = account.Balance + account.OverdraftLimit;

            if (amount > limit)
            {
                throw new Exception("Amount exceeds account limit.");
            }

            accountData.Withdraw(accountNumber, amount);

            transactionData.AddTransaction(new TransactionRecord
            {
                SourceAccountNumber = accountNumber,
                DestinationAccountNumber = null,
                TransactionType = "Withdraw",
                Amount = amount,
                ReferenceNumber = Guid.NewGuid().ToString(),
                TransactionDate = DateTime.Now
            });
        }

        public void Transfer(int sourceAccountNumber, int destinationAccountNumber, decimal amount, bool isExternal)
        {
            Account sourceAccount = accountData.GetAccountByNumber(sourceAccountNumber);

            if (sourceAccount == null)
            {
                throw new Exception("Source account not found.");
            }

            if (amount <= 0)
            {
                throw new Exception("Transfer amount must be greater than zero.");
            }

            decimal limit = sourceAccount.Balance + sourceAccount.OverdraftLimit;

            if (amount > limit)
            {
                throw new Exception("Transfer exceeds available limit.");
            }

            if (sourceAccount.AccountType == "Savings" && isExternal)
            {
                throw new Exception("Savings accounts can only transfer internally.");
            }

            accountData.Withdraw(sourceAccountNumber, amount);

            if (!isExternal)
            {
                Account destinationAccount = accountData.GetAccountByNumber(destinationAccountNumber);

                if (destinationAccount == null)
                {
                    throw new Exception("Destination account not found.");
                }

                accountData.Deposit(destinationAccountNumber, amount);
            }

            transactionData.AddTransaction(new TransactionRecord
            {
                SourceAccountNumber = sourceAccountNumber,
                DestinationAccountNumber = isExternal ? (int?)null : destinationAccountNumber,
                TransactionType = "Transfer",
                Amount = amount,
                ReferenceNumber = Guid.NewGuid().ToString(),
                TransactionDate = DateTime.Now
            });
        }

        public void ExportAccountToXml(Account account, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Account));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, account);
            }
        }

        public void DeactivateAccount(int accountNumber)
        {
            accountData.DeactivateAccount(accountNumber);
        }
        public int GenerateAccountNumber()
        {
            Random random = new Random();
            int accountNumber;

            do
            {
                accountNumber = random.Next(10000000, 99999999);
            }
            while (GetAccountByNumber(accountNumber) != null);

            return accountNumber;
        }
    }
}
using System.Collections.Generic;
using banking.Data;
using banking.Models;

namespace banking.Business
{
    public class TransactionService
    {
        private readonly TransactionData transactionData = new TransactionData();

        public List<TransactionRecord> GetTransactionsByAccount(int accountNumber)
        {
            return transactionData.GetTransactionsByAccount(accountNumber);
        }
    }
}
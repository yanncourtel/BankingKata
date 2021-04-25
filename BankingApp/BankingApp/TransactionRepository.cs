using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{
    public class TransactionRepository : ITransactionRepository
    {
        public List<Transaction> _accountTransactions = new List<Transaction>();

        public List<Transaction> GetAll()
        {
            return _accountTransactions;
        }

        public void Add(Transaction transaction)
        {
            _accountTransactions.Add(transaction);
        }
    }
}
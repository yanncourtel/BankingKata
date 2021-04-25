using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{
    public class TransactionRepository : ITransactionRepository
    {
        public List<AccountTransaction> _accountTransactions = new List<AccountTransaction>();

        public List<AccountTransaction> GetAll()
        {
            return _accountTransactions;
        }

        public void Add(AccountTransaction transaction)
        {
            _accountTransactions.Add(transaction);
        }
    }
}
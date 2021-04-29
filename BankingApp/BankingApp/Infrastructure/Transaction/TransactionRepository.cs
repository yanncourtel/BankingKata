using System.Collections.Generic;

using BankingApp.Domain.Transaction;

namespace BankingApp.Infrastructure.Transaction
{
    public class TransactionRepository : ITransactionRepository
    {
        public List<Domain.Transaction.Transaction> _accountTransactions = new List<Domain.Transaction.Transaction>();

        public List<Domain.Transaction.Transaction> GetAll()
        {
            return _accountTransactions;
        }

        public void Add(Domain.Transaction.Transaction transaction)
        {
            _accountTransactions.Add(transaction);
        }
    }
}
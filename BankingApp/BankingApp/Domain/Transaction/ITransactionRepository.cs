using System.Collections.Generic;

namespace BankingApp.Domain.Transaction
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();

        void Add(Transaction transaction);
    }
}
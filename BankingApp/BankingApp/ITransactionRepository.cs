using System.Collections.Generic;

namespace BankingApp
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();

        void Add(Transaction transaction);
    }
}
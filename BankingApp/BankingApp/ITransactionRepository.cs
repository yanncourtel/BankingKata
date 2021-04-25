using System.Collections.Generic;

namespace BankingApp
{
    public interface ITransactionRepository
    {
        List<AccountTransaction> GetAll();

        void Add(AccountTransaction transaction);
    }
}
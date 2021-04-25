using System.Collections.Generic;

namespace BankingApp
{
    public interface IStatementPrinter
    {
        void Print(List<Transaction> transactions);
    }
}
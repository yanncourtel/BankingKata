using System.Collections.Generic;

namespace BankingApp.Domain.Statement
{
    public interface IStatementPrinter
    {
        void Print(List<Transaction.Transaction> transactions);
    }
}
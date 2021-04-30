using System.Collections.Generic;
using System.Linq;

namespace BankingApp.Domain.Statement
{
    public class StatementPrinter : IStatementPrinter
    {
        private const string HeaderStatement = "Date\t\tAmount\t\tBalance";

        private readonly IOutputAdapter console;

        public StatementPrinter(IOutputAdapter console)
        {
            this.console = console;
        }

        public void Print(List<Transaction.Transaction> transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                console.Send("You have not made any transactions");
                return;
            }

            console.Send(HeaderStatement);

            var transactionsToBeSent = new List<StatementTransactionLine>();
            var runningBalance = 0;

            foreach (var transaction in transactions)
            {
                runningBalance += transaction.Amount;

                transactionsToBeSent.Add(new StatementTransactionLine(transaction, runningBalance));
            }

            SendTransactionsToConsole(transactionsToBeSent);
        }

        private void SendTransactionsToConsole(List<StatementTransactionLine> transactionsToBeSent)
        {
            transactionsToBeSent.Reverse();
            transactionsToBeSent.ForEach(t => console.Send(t));
        }
    }
}

using System.Collections.Generic;
using System.Linq;

using BankingApp.Domain.Date;
using BankingApp.Domain.Transaction;

namespace BankingApp.Domain.Statement
{
    public class StatementPrinter : IStatementPrinter
    {
        private const string HeaderStatement = "Date\t\tAmount\t\tBalance";

        private readonly ICanRenderDate dateRenderer;
        private readonly IOutputAdapter console;

        public StatementPrinter(ICanRenderDate dateRenderer, IOutputAdapter console)
        {
            this.dateRenderer = dateRenderer;
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

            var transactionsToBeSent = new List<TransactionLine>();
            var runningBalance = 0;

            foreach (var transaction in transactions)
            {
                runningBalance += transaction.Amount;

                transactionsToBeSent.Add(CreateTransactionLine(transaction, runningBalance));
            }

            SendTransactionsToConsole(transactionsToBeSent);
        }

        private void SendTransactionsToConsole(List<TransactionLine> transactionsToBeSent)
        {
            transactionsToBeSent.Reverse();
            transactionsToBeSent.ForEach(t => console.Send(t));
        }

        private TransactionLine CreateTransactionLine(Transaction.Transaction transaction, int runningBalance)
        {
            return new TransactionLine
            {
                Amount = $"{transaction.Amount}",
                Date = $"{dateRenderer.RenderDate(transaction.Date)}",
                RunningBalance = $"{runningBalance}"
            };
        }
    }
}

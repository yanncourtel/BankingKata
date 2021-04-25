using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{
    public class StatementPrinter : IStatementPrinter
    {
        private readonly ICanRenderDate dateRenderer;
        private readonly IOutputAdapter console;

        public StatementPrinter(ICanRenderDate dateRenderer, IOutputAdapter console)
        {
            this.dateRenderer = dateRenderer;
            this.console = console;
        }

        public void Print(List<Transaction> transactions)
        {
            if (transactions == null || !transactions.Any())
            {
                console.Send("You have not made any transactions");
                return;
            }

            console.Send("Date\t\tAmount\t\tBalance");

            var transactionsToBeSent = new List<TransactionLine>();
            var runningBalance = 0;

            foreach (var transaction in transactions)
            {
                runningBalance += transaction.Amount;
                
                transactionsToBeSent.Add(new TransactionLine
                {
                    Amount = $"{transaction.Amount}",
                    Date = $"{dateRenderer.RenderDate(transaction.Date)}",
                    RunningBalance = $"{runningBalance}"
                });
            }

            transactionsToBeSent.Reverse();
            transactionsToBeSent.ForEach(t => console.Send(t));
        }
    }
}

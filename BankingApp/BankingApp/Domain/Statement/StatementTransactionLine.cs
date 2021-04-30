using BankingApp.Domain.Helpers;

namespace BankingApp.Domain.Statement
{
    public class StatementTransactionLine
    {
        public StatementTransactionLine(Transaction.Transaction sourceTransaction, int runningBalance)
        {
            Amount = $"{sourceTransaction.Amount}";
            Date = $"{DateRenderer.RenderDate(sourceTransaction.Date)}";
            RunningBalance = $"{runningBalance}";
        }

        public string Amount { get; set; }
        public string Date { get; set; }
        public string RunningBalance { get; set; }

        public override string ToString()
        {
            return $"\r\n{Date}" +
                 $"\t\t{Amount}" +
                 $"\t\t{RunningBalance}";
        }
    }
}
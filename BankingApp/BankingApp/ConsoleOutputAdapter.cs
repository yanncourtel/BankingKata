using System;
using System.Text;

namespace BankingApp
{
    public class ConsoleOutputAdapter : IOutputAdapter
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }

        public void Send(TransactionLine transactionLine)
        {
            //statement.Append(
            //    $"\n{dateRenderer.RenderDate(transaction.Date)}" +
            //    $"\t\t{transaction.Amount}" +
            //    $"\t\t{runningBalance}");

            Console.WriteLine(transactionLine);
        }
    }
}
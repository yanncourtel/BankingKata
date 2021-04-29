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
            Console.WriteLine(transactionLine);
        }
    }
}
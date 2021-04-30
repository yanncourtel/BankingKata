using System;
using System.Diagnostics.CodeAnalysis;

using BankingApp.Domain.Statement;

namespace BankingApp.Infrastructure
{
    public class ConsoleOutputAdapter : IOutputAdapter
    {
        public void Send(string message)
        {
            SendToConsole(message);
        }

        public void Send(StatementTransactionLine transactionLine)
        {
            SendToConsole(transactionLine.ToString());
        }

        [ExcludeFromCodeCoverage]
        protected virtual void SendToConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
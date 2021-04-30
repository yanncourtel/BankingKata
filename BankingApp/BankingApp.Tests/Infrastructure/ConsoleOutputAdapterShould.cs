using System;

using BankingApp.Domain.Statement;
using BankingApp.Domain.Transaction;

using FluentAssertions;

using Xunit;

namespace BankingApp.Tests.Infrastructure
{
    public class ConsoleOutputAdapterShould
    {
        [Fact]
        public void Send_Message_To_Console()
        {
            var expectedMessage = "here is the message";
            var outputAdapter = new TestableConsoleOutputAdapter();

            outputAdapter.Send(expectedMessage);

            outputAdapter.Console.ToString().Should().Be(expectedMessage);
        }

        [Fact]
        public void Send_Transaction_Line_To_Console()
        {
            var transactionLine = new StatementTransactionLine(
                new Transaction { Amount = 500, Date = new DateTime(2021, 03, 26) }, 500);

            var expectedMessage = transactionLine.ToString();
            var outputAdapter = new TestableConsoleOutputAdapter();

            outputAdapter.Send(transactionLine);

            outputAdapter.Console.ToString().Should().Be(expectedMessage);
        }
    }
}

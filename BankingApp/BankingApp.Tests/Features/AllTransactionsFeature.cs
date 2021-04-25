using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingApp.Tests.Features
{
    /// <summary>
    /// This scenario checks the statement printed in the console after doing several transactions.
    /// </summary>
    public class AllTransactionsFeature
    {
        private readonly IAccount account;
        private readonly Mock<IOutputAdapter> fakeConsole = new Mock<IOutputAdapter>();
        private string consoleResult = string.Empty;

        public AllTransactionsFeature()
        {
            ICanRenderDate dateRenderer = new DateRenderer();
            ITransactionRepository  transactionRepository= new TransactionRepository();

            fakeConsole
                .Setup(x => x.Send(It.IsAny<TransactionLine>()))
                .Callback((TransactionLine transactionLine) =>
                {
                    consoleResult += transactionLine.ToString();
                });

            fakeConsole
                .Setup(x => x.Send(It.IsAny<string>()))
                .Callback((string message) =>
                {
                    consoleResult += message;
                });

            account = new Account(dateRenderer, fakeConsole.Object, transactionRepository);
        }

        [Fact]
        public void Be_Able_To_Print_Statement_With_All_Transactions()
        {
            var expectedStatement =
                $"Date\t\tAmount\t\tBalance\n" +
                $"{DateTime.Today:d.M.yyyy}\t\t-200\t\t300\n" +
                $"{DateTime.Today:d.M.yyyy}\t\t500\t\t500";

            account.Deposit(500);
            account.Withdraw(200);
            account.PrintStatement();
            
            consoleResult.Should().Be(expectedStatement);
        }
    }
}

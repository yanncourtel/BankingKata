using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingApp.Tests
{
    public class AccountShould
    {
        private readonly IAccount account;
        private readonly Mock<IOutputAdapter> fakeConsole = new Mock<IOutputAdapter>();
        private string consoleResult = string.Empty;

        public AccountShould()
        {
            ICanRenderDate dateRenderer = new DateRenderer();

            fakeConsole
                .Setup(x => x.Send(It.IsAny<string>()))
                .Callback((string messageSent) =>
                {
                    consoleResult = messageSent;
                });

            account = new Account(dateRenderer, fakeConsole.Object);
        }

        [Fact]
        public void Be_Able_To_Print_Statement()
        {
            account.PrintStatement();

            consoleResult.Should().Be("Date\t\tAmount\t\tBalance");
        }

        [Fact]
        public void Be_Able_To_Print_Statement_With_Deposit_Transactions()
        {
            var expectedStatement =
                $"Date\t\tAmount\t\tBalance\n" +
                $"{DateTime.Today:d.M.yyyy}\t\t+500\t\t500";

            account.Deposit(500);
            account.PrintStatement();
            
            consoleResult.Should().Be(expectedStatement);
        }

        [Fact]
        public void Be_Able_To_Print_Statement_With_Withdraw_Transactions()
        {
            var expectedStatement =
                $"Date\t\tAmount\t\tBalance\n" +
                $"{DateTime.Today:d.M.yyyy}\t\t-200\t\t-200";

            account.Withdraw(200);
            account.PrintStatement();
            
            consoleResult.Should().Be(expectedStatement);
        }

        [Fact]
        public void Be_Able_To_Print_Statement_With_All_Transactions()
        {
            var expectedStatement =
                $"Date\t\tAmount\t\tBalance\n" +
                $"{DateTime.Today:d.M.yyyy}\t\t+500\t\t500\n" +
                $"{DateTime.Today:d.M.yyyy}\t\t-200\t\t300";

            account.Deposit(500);
            account.Withdraw(200);
            account.PrintStatement();
            
            consoleResult.Should().Be(expectedStatement);
        }
    }
}

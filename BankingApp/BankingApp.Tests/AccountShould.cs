using System;
using FluentAssertions;
using Xunit;

namespace BankingApp.Tests
{
    public class AccountShould
    {
        private readonly Account account;

        public AccountShould()
        {
            ICanRenderDate dateRenderer = new DateRenderer();
            account = new Account(dateRenderer);
        }

        [Fact]
        public void Be_Able_To_Print_Statement()
        {
            var statement = account.PrintStatement();
            statement.Should().Be("Date\t\tAmount\t\tBalance");
        }

        [Fact]
        public void Show_Balance()
        {
            var balance = account.ShowBalance();
            balance.Should().Be(0);
        }

        [Fact]
        public void Be_Able_To_Deposit_Money()
        {
            var expectedAmount = 500;

            account.Deposit(expectedAmount);
            var balance = account.ShowBalance();
            
            balance.Should().Be(expectedAmount);
        }

        [Fact]
        public void Be_Able_To_Withdraw_Money()
        {
            var depositAmount = 500;
            var withdrawAmount = 200;
            var expectedAmount = depositAmount - withdrawAmount;

            account.Deposit(depositAmount);
            account.Withdraw(withdrawAmount);
            var balance = account.ShowBalance();
            
            balance.Should().Be(expectedAmount);
        }

        [Fact]
        public void Today_Date_Printed()
        {
            DateTime.Today.ToString("d.M.yyyy").Should().Be("20.4.2021");
        }

        [Fact]
        public void Be_Able_To_Print_Statement_With_Deposit_Transactions()
        {
            var expectedStatement =
                $"Date\t\tAmount\t\tBalance\n" +
                $"20.4.2021\t\t+500\t\t500";

            account.Deposit(500);
            var statement = account.PrintStatement();
            
            statement.Should().Be(expectedStatement);
        }

        [Fact]
        public void Be_Able_To_Print_Statement_With_All_Transactions()
        {
            var expectedStatement =
                $"Date\t\tAmount\t\tBalance\n" +
                $"20.4.2021\t\t+500\t\t500\n" +
                $"20.4.2021\t\t-200\t\t300";

            account.Deposit(500);
            account.Withdraw(200);
            var statement = account.PrintStatement();
            
            statement.Should().Be(expectedStatement);
        }
    }
}

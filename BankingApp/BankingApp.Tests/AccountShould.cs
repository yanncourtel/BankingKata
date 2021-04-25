using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingApp.Tests.Features
{
    public class AccountShould
    {
        private readonly IAccount account;
        private readonly Mock<IOutputAdapter> fakeConsole = new Mock<IOutputAdapter>();
        private readonly Mock<ITransactionRepository> fakeRepository = new Mock<ITransactionRepository>();

        public AccountShould()
        {
            ICanRenderDate dateRenderer = new DateRenderer();

            account = new Account(dateRenderer, fakeConsole.Object, fakeRepository.Object);
        }

        [Fact]
        public void Record_Transaction_After_A_Deposit()
        {
            var expectedTransaction = new AccountTransaction()
            {
                Amount = 500,
                Date = DateTime.Today
            };

            AccountTransaction actualTransactionStored = null;
            fakeRepository
                .Setup(x => x.Add(It.IsAny<AccountTransaction>()))
                .Callback((AccountTransaction transaction) =>
                {
                    actualTransactionStored = transaction;
                });

            account.Deposit(500);

            actualTransactionStored.Should().BeEquivalentTo(expectedTransaction);
        }

        [Fact]
        public void Record_Transaction_After_A_Withdrawal()
        {
            var expectedTransaction = new AccountTransaction()
            {
                Amount = -100,
                Date = DateTime.Today
            };

            AccountTransaction actualTransactionStored = null;
            fakeRepository
                .Setup(x => x.Add(It.IsAny<AccountTransaction>()))
                .Callback((AccountTransaction transaction) =>
                {
                    actualTransactionStored = transaction;
                });

            account.Withdraw(100);

            actualTransactionStored.Should().BeEquivalentTo(expectedTransaction);
        }

        [Fact]
        public void Print_Recorded_Transactions()
        {
            var expectedTransactionLines = new List<TransactionLine>
            {
                new TransactionLine { Amount = "500", Date = $"{DateTime.Today:d.M.yyyy}", RunningBalance = "500" },
                new TransactionLine { Amount = "-100", Date = $"{DateTime.Today:d.M.yyyy}", RunningBalance = "400" },
                new TransactionLine { Amount = "-150", Date = $"{DateTime.Today:d.M.yyyy}", RunningBalance = "250" }
            };

            var storedTransactions = new List<AccountTransaction>
            {
                new AccountTransaction { Amount = 500, Date = DateTime.Today },
                new AccountTransaction { Amount = -100, Date = DateTime.Today },
                new AccountTransaction { Amount = -150, Date = DateTime.Today }
            };
            
            fakeRepository
                .Setup(x => x.GetAll())
                .Returns(storedTransactions);

            var transactionsSent = new List<TransactionLine>();
            fakeConsole
                .Setup(x => x.Send(It.IsAny<TransactionLine>()))
                .Callback((TransactionLine transactionSent) =>
                {
                    transactionsSent.Insert(0, transactionSent);
                });

            account.PrintStatement();

            transactionsSent.Should().BeEquivalentTo(expectedTransactionLines);
        }

        [Fact]
        public void Print_Notice_If_No_Recorded_Transactions()
        {
            fakeRepository
                .Setup(x => x.GetAll())
                .Returns(new List<AccountTransaction>());

            var consoleResult = string.Empty;
            fakeConsole
                .Setup(x => x.Send(It.IsAny<string>()))
                .Callback((string message) =>
                {
                    consoleResult += message;
                });

            account.PrintStatement();

            consoleResult.Should().Be("You have not made any transactions");
        }
    }
}

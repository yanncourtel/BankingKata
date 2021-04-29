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
        private readonly Mock<ITransactionRepository> fakeRepository = new Mock<ITransactionRepository>();
        private readonly Mock<IStatementPrinter> fakeStatementPrinter = new Mock<IStatementPrinter>();
        private readonly Mock<IClock> clock = new Mock<IClock>();

        public AccountShould()
        {
            account = new Account(fakeStatementPrinter.Object, fakeRepository.Object, clock.Object);
        }

        [Fact]
        public void Record_Transaction_After_A_Deposit()
        {
            var expectedTodayDate = new DateTime(2021, 04, 27);
            SetupTodayDate(expectedTodayDate);
            var expectedTransaction = new Transaction()
            {
                Amount = 500,
                Date = expectedTodayDate
            };

            Transaction actualTransactionStored = null;
            fakeRepository
                .Setup(x => x.Add(It.IsAny<Transaction>()))
                .Callback((Transaction transaction) =>
                {
                    actualTransactionStored = transaction;
                });

            account.Deposit(500);

            actualTransactionStored.Should().BeEquivalentTo(expectedTransaction);
        }

        [Fact]
        public void Record_Transaction_After_A_Withdrawal()
        {
            var expectedTodayDate = new DateTime(2021, 04, 27);
            SetupTodayDate(expectedTodayDate);
            var expectedTransaction = new Transaction()
            {
                Amount = -100,
                Date = expectedTodayDate
            };

            Transaction actualTransactionStored = null;
            fakeRepository
                .Setup(x => x.Add(It.IsAny<Transaction>()))
                .Callback((Transaction transaction) =>
                {
                    actualTransactionStored = transaction;
                });

            account.Withdraw(100);

            actualTransactionStored.Should().BeEquivalentTo(expectedTransaction);
        }

        [Fact]
        public void Print_Recorded_Transactions()
        {
            var expectedTodayDate = new DateTime(2021, 04, 27);
            SetupTodayDate(expectedTodayDate);
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { Amount = 500, Date = expectedTodayDate },
                new Transaction { Amount = -100, Date = expectedTodayDate },
                new Transaction { Amount = -150, Date = expectedTodayDate }
            };
            
            fakeRepository
                .Setup(x => x.GetAll())
                .Returns(expectedTransactions);

            var actualTransactions = new List<Transaction>();
            fakeStatementPrinter
                .Setup(x => x.Print(It.IsAny<List<Transaction>>()))
                .Callback((List<Transaction> transactions) =>
                {
                    actualTransactions = transactions;
                });

            account.PrintStatement();

            actualTransactions.Should().BeEquivalentTo(expectedTransactions);
        }

        private void SetupTodayDate(DateTime expectedTodayDate)
        {
            clock.Setup(x => x.GetTodayDate())
                .Returns(expectedTodayDate);
        }
    }
}

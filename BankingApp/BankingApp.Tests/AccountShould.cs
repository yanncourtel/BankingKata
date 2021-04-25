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

        public AccountShould()
        {
            account = new Account(fakeStatementPrinter.Object, fakeRepository.Object);
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
            var expectedTransactions = new List<AccountTransaction>
            {
                new AccountTransaction { Amount = 500, Date = DateTime.Today },
                new AccountTransaction { Amount = -100, Date = DateTime.Today },
                new AccountTransaction { Amount = -150, Date = DateTime.Today }
            };
            
            fakeRepository
                .Setup(x => x.GetAll())
                .Returns(expectedTransactions);

            var actualTransactions = new List<AccountTransaction>();
            fakeStatementPrinter
                .Setup(x => x.Print(It.IsAny<List<AccountTransaction>>()))
                .Callback((List<AccountTransaction> transactions) =>
                {
                    actualTransactions = transactions;
                });

            account.PrintStatement();

            actualTransactions.Should().BeEquivalentTo(expectedTransactions);
        }
    }
}

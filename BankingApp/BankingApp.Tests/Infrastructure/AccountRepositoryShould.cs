using System;
using System.Collections.Generic;

using BankingApp.Domain.Transaction;
using BankingApp.Infrastructure.Transaction;

using FluentAssertions;

using Xunit;

namespace BankingApp.Tests.Infrastructure
{
    public class AccountRepositoryShould
    {
        private readonly TransactionRepository repository;

        public AccountRepositoryShould()
        {
            repository = new TransactionRepository();
        }

        [Fact]
        public void Add_A_Transaction()
        {
            var expectedTransaction = new Transaction()
            {
                Amount = 500,
                Date = DateTime.Today
            };

            repository.Add(expectedTransaction);

            repository._accountTransactions.Count.Should().Be(1);
            repository._accountTransactions.Should().ContainEquivalentOf(expectedTransaction);
        }

        [Fact]
        public void Get_All_Transaction()
        {
            var expectedTransactions = new List<Transaction>
            {
                new Transaction {Amount = 500, Date = DateTime.Today},
                new Transaction {Amount = 200, Date = DateTime.Today},
                new Transaction {Amount = 100, Date = DateTime.Today}
            };
            expectedTransactions.ForEach(t => repository._accountTransactions.Add(t));

            var actualTransactions = repository.GetAll();

            actualTransactions.Should().BeEquivalentTo(expectedTransactions);
        }
    }
}

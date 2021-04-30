using System;
using System.Text;

using BankingApp.Domain.Account;
using BankingApp.Domain.Clock;
using BankingApp.Domain.Helpers;
using BankingApp.Domain.Statement;
using BankingApp.Domain.Transaction;
using BankingApp.Infrastructure.Transaction;

using FluentAssertions;

using Moq;

using TechTalk.SpecFlow;

namespace BankingApp.Tests.BDD.Steps
{
    [Binding]
    public sealed class AccountStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private readonly Mock<IOutputAdapter> _fakeOutput = new Mock<IOutputAdapter>();
        private readonly Mock<IClock> _clock = new Mock<IClock>();
        private readonly Account _account;

        private readonly StringBuilder _fakeConsole = new StringBuilder();

        public AccountStepDefinitions()
        {
            SetupFakeConsole();

            var statementPrinter = new StatementPrinter(_fakeOutput.Object);
            var repository = new TransactionRepository();

            _account = new Account(statementPrinter, repository, _clock.Object);
        }

        [Given(@"a client makes a deposit (.*) euros on the (.*)")]
        public void GivenAClientMakesADepositEurosOnThe(int amount, DateTime dateDeposit)
        {
            SetupTodayDate(dateDeposit);

            _account.Deposit(amount);
        }

        [Given(@"a client withdraws (.*) euros on the (.*)")]
        public void GivenAClientWithdrawsEurosOnThe(int amount, DateTime dateWithdrawal)
        {
            SetupTodayDate(dateWithdrawal);

            _account.Withdraw(amount);
        }


        [When(@"she prints her bank statement")]
        public void WhenShePrintsHerBankStatement()
        {
            _account.PrintStatement();
        }

        [Then(@"she should see the following statement:")]
        public void ThenSheShouldSeeTheFollowingStatement(string statementPrinted)
        {
            _fakeConsole.ToString().Should().BeEquivalentTo(statementPrinted);
        }

        private void SetupFakeConsole()
        {
            _fakeOutput
                .Setup(x => x.Send(It.IsAny<string>()))
                .Callback((string message) => AddMessageToConsole(message));

            _fakeOutput
                .Setup(x => x.Send(It.IsAny<StatementTransactionLine>()))
                .Callback((StatementTransactionLine transactionLine) => { AddMessageToConsole(transactionLine.ToString()); });
        }

        private void SetupTodayDate(DateTime expectedTodayDate)
        {
            _clock
                .Setup(x => x.GetTodayDate())
                .Returns(expectedTodayDate);
        }

        private void AddMessageToConsole(string message)
        {
            _fakeConsole.Append(message);
        }
    }
}

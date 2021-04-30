using System;
using System.Text;

using BankingApp.Domain.Statement;
using BankingApp.Domain.Transaction;

using Castle.Components.DictionaryAdapter;

using FluentAssertions;

using Moq;

using Xunit;

namespace BankingApp.Tests.Domain
{
    public class StatementPrinterShould
    {
        private readonly StringBuilder _fakeConsole = new StringBuilder();
        private readonly Mock<IOutputAdapter> _fakeOutput = new Mock<IOutputAdapter>();
        private readonly StatementPrinter _statementPrinter;

        public StatementPrinterShould()
        {
            SetupFakeConsole();

            _statementPrinter = new StatementPrinter(_fakeOutput.Object);
        }

        [Fact]
        public void Print_A_Statement_With_One_Transaction()
        {
            var expected = $"Date\t\tAmount\t\tBalance\r\n27.4.2021\t\t500\t\t500";

            _statementPrinter.Print(new EditableList<Transaction>
            {
                new Transaction { Amount = 500, Date = new DateTime(2021, 04, 27)}
            });

            _fakeConsole.ToString().Should().Be(expected);
        }

        [Fact]
        public void Print_A_Statement_With_Two_Transactions()
        {
            var expected = $"Date\t\tAmount\t\tBalance\r\n27.4.2021\t\t-100\t\t400\r\n25.4.2021\t\t500\t\t500";

            _statementPrinter.Print(new EditableList<Transaction>
            {
                new Transaction { Amount = 500, Date = new DateTime(2021, 04, 25)},
                new Transaction { Amount = -100, Date = new DateTime(2021, 04, 27)}
            });

            _fakeConsole.ToString().Should().Be(expected);
        }

        [Fact]
        public void Print_A_Default_Message_If_There_Was_No_Transactions_Made()
        {
            var expected = "You have not made any transactions";

            _statementPrinter.Print(new EditableList<Transaction>());

            _fakeConsole.ToString().Should().Be(expected);
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

        private void AddMessageToConsole(string message)
        {
            _fakeConsole.Append(message);
        }
    }
}

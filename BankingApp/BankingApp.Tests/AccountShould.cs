using FluentAssertions;
using Xunit;

namespace BankingApp.Tests
{
    public class AccountShould
    {
        [Fact]
        public void PrintStatement()
        {
            var account = new Account();
            var statement = account.PrintStatement();
            statement.Should().Be("Date\t\tAmount\t\tBalance");
        }

        public class Account
        {
            public string PrintStatement()
            {
                return "Date\t\tAmount\t\tBalance";
            }
        }
    }
}

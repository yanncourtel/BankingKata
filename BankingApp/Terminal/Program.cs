using BankingApp.Domain.Account;
using BankingApp.Domain.Clock;
using BankingApp.Domain.Date;
using BankingApp.Domain.Statement;
using BankingApp.Infrastructure;
using BankingApp.Infrastructure.Transaction;

namespace BankingApp.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var clock = new Clock();
            var dateRenderer = new DateRenderer();
            var consoleAdapter = new ConsoleOutputAdapter();
            var statementPrinter = new StatementPrinter(dateRenderer, consoleAdapter);
            var transactionRepository = new TransactionRepository();
            var account = new Account(statementPrinter, transactionRepository, clock);

            account.Deposit(500);
            account.Withdraw(100);
            account.Withdraw(50);
            account.Withdraw(27);
            account.Deposit(250);
            account.Withdraw(123);

            account.PrintStatement();
        }
    }
}

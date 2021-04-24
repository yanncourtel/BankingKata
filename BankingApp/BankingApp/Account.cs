using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    public class Account : IAccount
    {
        private readonly ICanRenderDate dateRenderer;
        private readonly IOutputAdapter console;
        private int balance;

        private readonly List<AccountTransaction> transactions;

        public Account(ICanRenderDate dateRenderer, IOutputAdapter console)
        {
            this.dateRenderer = dateRenderer;
            this.console = console;
            balance = 0;
            transactions = new List<AccountTransaction>();
        }

        public void PrintStatement()
        {
            StringBuilder statement = new StringBuilder("Date\t\tAmount\t\tBalance");

            foreach (var transaction in transactions)
            {
                statement.Append(
                    $"\n{dateRenderer.RenderDate(transaction.Date)}\t\t{transaction.Operator}{transaction.Amount}\t\t{transaction.Balance}");
            }

            console.Send(statement.ToString());
        }

        public void Deposit(int amount)
        {
            balance += amount;

            RegisterTransaction(amount, false);
        }

        public void Withdraw(int amount)
        {
            balance -= amount;

            RegisterTransaction(amount, true);
        }

        private void RegisterTransaction(int amount, bool withdrawing)
        {
            transactions.Insert(0, new AccountTransaction
            {
                Amount = amount,
                Balance = balance,
                Operator = withdrawing ? '-' : '+',
                Date = DateTime.Today
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp
{
    public class Account
    {
        private readonly ICanRenderDate dateRenderer;
        private int balance;

        private readonly List<AccountTransaction> transactions;

        public Account(ICanRenderDate dateRenderer)
        {
            this.dateRenderer = dateRenderer;
            balance = 0;
            transactions = new List<AccountTransaction>();
        }

        public string PrintStatement()
        {
            StringBuilder statement = new StringBuilder("Date\t\tAmount\t\tBalance");

            foreach (var transaction in transactions)
            {
                statement.Append(
                    $"\n{dateRenderer.RenderDate(transaction.Date)}\t\t{transaction.Operator}{transaction.Amount}\t\t{transaction.Balance}");
            }

            return statement.ToString();
        }

        public double ShowBalance()
        {
            return balance;
        }

        public void Deposit(int amount)
        {
            balance += amount;

            transactions.Add(new AccountTransaction
            {
                Amount = amount,
                Balance = balance,
                Operator = '+',
                Date = DateTime.Today
            });
        }

        public void Withdraw(in int amount)
        {
            balance -= amount;

            transactions.Add(new AccountTransaction
            {
                Amount = amount,
                Balance = balance,
                Operator = '-',
                Date = DateTime.Today
            });
        }
    }
}
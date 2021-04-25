using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingApp
{
    public class Account : IAccount
    {
        private readonly ICanRenderDate dateRenderer;
        private readonly IOutputAdapter console;

        private readonly ITransactionRepository _transactionRepository;

        public Account(ICanRenderDate dateRenderer, IOutputAdapter console, ITransactionRepository transactionRepository)
        {
            this.dateRenderer = dateRenderer;
            this.console = console;
            _transactionRepository = transactionRepository;
        }

        public void PrintStatement()
        {
            var storedTransactions = _transactionRepository.GetAll();
            if (storedTransactions == null || !storedTransactions.Any())
            {
                console.Send("You have not made any transactions");
                return;
            }

            console.Send("Date\t\tAmount\t\tBalance");

            var transactionsToBeSent = new List<TransactionLine>();
            var runningBalance = 0;

            foreach (var transaction in storedTransactions)
            {
                runningBalance += transaction.Amount;
                
                transactionsToBeSent.Add(new TransactionLine
                {
                    Amount = $"{transaction.Amount}",
                    Date = $"{dateRenderer.RenderDate(transaction.Date)}",
                    RunningBalance = $"{runningBalance}"
                });
            }

            transactionsToBeSent.Reverse();
            transactionsToBeSent.ForEach(t => console.Send(t));
        }

        public void Deposit(int amount)
        {
            _transactionRepository.Add(GenerateTransaction(amount));
        }

        public void Withdraw(int amount)
        {
            _transactionRepository.Add(GenerateTransaction(-amount));
        }

        private static AccountTransaction GenerateTransaction(int amount)
        {
            return new AccountTransaction
            {
                Amount = amount,
                Date = DateTime.Today
            };
        }
    }
}
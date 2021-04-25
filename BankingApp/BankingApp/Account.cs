using System;

namespace BankingApp
{
    public class Account : IAccount
    {
        private readonly IStatementPrinter _statementPrinter;
        private readonly ITransactionRepository _transactionRepository;

        public Account(IStatementPrinter statementPrinter, ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            _statementPrinter = statementPrinter;
        }

        public void PrintStatement()
        {
            _statementPrinter.Print(_transactionRepository.GetAll());
        }

        public void Deposit(int amount)
        {
            _transactionRepository.Add(GenerateTransaction(amount));
        }

        public void Withdraw(int amount)
        {
            _transactionRepository.Add(GenerateTransaction(-amount));
        }

        private static Transaction GenerateTransaction(int amount)
        {
            return new Transaction
            {
                Amount = amount,
                Date = DateTime.Today
            };
        }
    }
}
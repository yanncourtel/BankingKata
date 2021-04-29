namespace BankingApp
{
    public class Account : IAccount
    {
        private readonly IStatementPrinter _statementPrinter;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClock _clock;

        public Account(IStatementPrinter statementPrinter, ITransactionRepository transactionRepository, IClock clock)
        {
            _transactionRepository = transactionRepository;
            _clock = clock;
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

        private Transaction GenerateTransaction(int amount)
        {
            return new Transaction
            {
                Amount = amount,
                Date = _clock.GetTodayDate()
            };
        }
    }
}
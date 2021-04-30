namespace BankingApp.Domain.Statement
{
    public interface IOutputAdapter
    {
        void Send(string message);
        void Send(StatementTransactionLine transactionLine);
    }
}
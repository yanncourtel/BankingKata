namespace BankingApp
{
    public interface IOutputAdapter
    {
        void Send(string message);
        void Send(TransactionLine transactionLine);
    }
}
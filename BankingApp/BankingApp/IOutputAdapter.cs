namespace BankingApp
{
    public interface IOutputAdapter
    {
        void Send(string message);
    }
}
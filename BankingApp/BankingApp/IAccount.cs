namespace BankingApp
{
    public interface IAccount
    {
        public void Deposit(int amount);
        public void Withdraw(int amount);
        public void PrintStatement();
    }
}
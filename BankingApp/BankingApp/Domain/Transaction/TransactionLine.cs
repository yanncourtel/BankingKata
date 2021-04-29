namespace BankingApp.Domain.Transaction
{
    public class TransactionLine
    {
        public string Amount { get; set; }
        public string Date { get; set; }
        public string RunningBalance { get; set; }

        public override string ToString()
        {
            return $"\r\n{Date}" +
                 $"\t\t{Amount}" +
                 $"\t\t{RunningBalance}";
        }
    }
}
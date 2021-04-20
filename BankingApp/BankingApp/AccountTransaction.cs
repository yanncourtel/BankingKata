using System;

namespace BankingApp
{
    internal class AccountTransaction
    {
        public int Balance { get; set; }

        public int Amount { get; set; }

        public char Operator { get; set; }

        public DateTime Date { get; set; }
    }
}
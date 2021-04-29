using System;

namespace BankingApp
{
    public class Clock : IClock
    {
        public DateTime GetTodayDate()
        {
            return DateTime.Today;
        }
    }
}
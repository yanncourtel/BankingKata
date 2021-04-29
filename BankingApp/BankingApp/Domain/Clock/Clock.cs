using System;

namespace BankingApp.Domain.Clock
{
    public class Clock : IClock
    {
        public DateTime GetTodayDate()
        {
            return DateTime.Today;
        }
    }
}
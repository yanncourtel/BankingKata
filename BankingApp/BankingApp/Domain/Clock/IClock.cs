using System;

namespace BankingApp.Domain.Clock
{
    public interface IClock
    {
        DateTime GetTodayDate();
    }
}
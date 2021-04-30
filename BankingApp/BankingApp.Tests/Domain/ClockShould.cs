using System;

using BankingApp.Domain.Clock;

using FluentAssertions;

using Xunit;

namespace BankingApp.Tests.Domain
{
    public class ClockShould
    {
        [Fact]
        public void Return_Today_Date()
        {
            var clock = new Clock();

            var todayDate = clock.GetTodayDate();

            todayDate.Should().Be(DateTime.Today);
        }
    }
}

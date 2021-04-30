using System;
using BankingApp.Domain.Helpers;
using FluentAssertions;
using Xunit;

namespace BankingApp.Tests.Domain
{
    public class DateRendererShould
    {
        [Fact]
        public void Format_A_Date_As_dd_MM_yyyy()
        {
            var dateToFormat = new DateTime(2020, 03, 20);

            var dateFormatted = DateRenderer.RenderDate(dateToFormat);

            dateFormatted.Should().Be("20.3.2020");
        }
    }
}

using System;

namespace BankingApp.Domain.Helpers
{
    public static class DateRenderer
    {
        public static string RenderDate(DateTime dateToRender)
        {
            return dateToRender.ToString("d.M.yyyy");
        }
    }
}
using System;

namespace BankingApp.Domain.Date
{
    public class DateRenderer : ICanRenderDate
    {
        public DateRenderer()
        {
        }

        public string RenderDate(DateTime dateToRender)
        {
            return dateToRender.ToString("d.M.yyyy");
        }
    }
}
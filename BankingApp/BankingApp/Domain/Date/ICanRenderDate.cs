using System;

namespace BankingApp.Domain.Date
{
    public interface ICanRenderDate
    {
        string RenderDate(DateTime dateToRender);
    }
}
using System;

namespace BankingApp
{
    public interface ICanRenderDate
    {
        string RenderDate(DateTime dateToRender);
    }
}
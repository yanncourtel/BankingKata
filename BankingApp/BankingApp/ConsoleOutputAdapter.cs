using System;

namespace BankingApp
{
    public class ConsoleOutputAdapter : IOutputAdapter
    {
        public void Send(string message)
        {
            Console.WriteLine(message);
        }
    }
}
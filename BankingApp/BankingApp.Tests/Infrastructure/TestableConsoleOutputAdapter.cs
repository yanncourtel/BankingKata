using System.Text;
using BankingApp.Infrastructure;

namespace BankingApp.Tests.Infrastructure
{
    public class TestableConsoleOutputAdapter : ConsoleOutputAdapter
    {
        public StringBuilder Console { get; set; }

        public TestableConsoleOutputAdapter()
        {
            Console = new StringBuilder();
        }

        protected override void SendToConsole(string message)
        {
            Console.Append(message);
        }
    }
}
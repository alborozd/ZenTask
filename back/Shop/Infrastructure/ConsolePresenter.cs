using Shop.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Infrastructure
{
    public class ConsolePresenter : IPresenter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}

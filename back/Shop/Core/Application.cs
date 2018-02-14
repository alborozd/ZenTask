using Shop.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core
{
    public class Application : IShopApplication
    {
        public void Run()
        {
            Console.WriteLine("Hey!");
            Console.ReadLine();
        }
    }
}

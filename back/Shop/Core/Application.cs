using Shop.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core
{
    public class Application : IShopApplication
    {
        private readonly IPresenter presenter;

        public Application(IPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void Run()
        {
            presenter.WriteLine("Hey");
            Console.ReadLine();
        }
    }
}

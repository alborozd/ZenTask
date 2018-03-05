using Autofac;
using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Infrastructure;
using System;

namespace Shop.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var scope = AutofacConfig.GetContainer().BeginLifetimeScope())
            {
                IPresenter presenter = scope.Resolve<IPresenter>();
                try
                {
                    IShopApplication app = scope.Resolve<IShopApplication>();
                    app.Run();
                }
                catch (Exception ex)
                {                    
                    presenter.WriteLine(ex.Message);
                }

                Console.ReadLine();
            }
        }
    }
}

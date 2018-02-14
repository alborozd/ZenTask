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
                IShopApplication app = scope.Resolve<IShopApplication>();
                app.Run();
            }
        }
    }
}

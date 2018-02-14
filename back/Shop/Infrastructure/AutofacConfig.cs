using Autofac;
using Shop.ConsoleClient.Core;
using Shop.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Infrastructure
{
    public class AutofacConfig
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IShopApplication>();

            return builder.Build();
        }
    }
}

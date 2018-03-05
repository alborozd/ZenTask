using Autofac;
using Shop.Common.Contracts;
using Shop.Common.Services;
using Shop.Common.Services.Contracts;
using Shop.ConsoleClient.Core;
using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.ConsoleClient.Core.Screens;
using Shop.Contracts.Dal;
using Shop.Logic.Facades;
using Shop.XmlDal;
using Shop.XmlDal.Contracts;
using Shop.XmlDal.Repositories;
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
            builder.RegisterType<ConsolePresenter>().As<IPresenter>();
            builder.RegisterType<DataBus>().As<IDataBus>().SingleInstance();

            //screens
            builder.RegisterType<MainScreen>().As<IMainScreen>();
            builder.RegisterType<SelectUserScreen>().As<ISelectUserScreen>();
            builder.RegisterType<InputUserScreen>().As<IInputUserScreen>();

            //facades
            builder.RegisterType<TransactionsFacade>();
            builder.RegisterType<UserFacade>();

            //repositories
            builder.RegisterType<DiscountsRepository>().As<IDiscountsRepository>();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>();
            builder.RegisterType<TransacionErrorsRepository>().As<ITransactionErrorsRepository>();
            builder.RegisterType<TransactionsRepository>().As<ITransactionsRepository>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();

            //services
            builder.RegisterType<Shop.Common.Environment>().As<IEnvironment>();
            builder.RegisterType<MutextLock>().As<ILockService>();
            builder.RegisterType<XmlPathResolver>().As<IXmlPathResolver>();

            return builder.Build();
        }
    }
}

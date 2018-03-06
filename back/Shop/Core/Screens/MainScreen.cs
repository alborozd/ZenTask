using System;
using System.Collections.Generic;
using System.Text;
using Shop.ConsoleClient.Core.Contracts;
using Autofac;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.Domain;
using Shop.ConsoleClient.Core.Exceptions;

namespace Shop.ConsoleClient.Core.Screens
{
    public class MainScreen : IMainScreen
    {
        private Lazy<ISelectUserScreen> selectUserScreen;
        private Lazy<IInputUserScreen> inputUserScreen;
        private Lazy<IProductsListScreen> productsScreen;
        private IPresenter presenter;
        private IDataBus dataBus; 

        public MainScreen(Func<ISelectUserScreen> selectUserScreen, Func<IInputUserScreen> inputUserScreen, Func<IProductsListScreen> productsScreen, IPresenter presenter, IDataBus dataBus)
        {
            this.selectUserScreen = new Lazy<ISelectUserScreen>(selectUserScreen);
            this.inputUserScreen = new Lazy<IInputUserScreen>(inputUserScreen);
            this.productsScreen = new Lazy<IProductsListScreen>(productsScreen);
            this.presenter = presenter;
            this.dataBus = dataBus;
        }

        public void Show()
        {
            presenter.WriteLine("1. Select user");
            presenter.WriteLine("2. Create user");
            presenter.WriteLine("3. Buy products");
            presenter.WriteLine("4. Exit");
        }

        public IScreen HandleInput(string input)
        {
            switch(input)
            {
                case "1": return selectUserScreen.Value;
                case "2": return inputUserScreen.Value;
                case "3":
                    {
                        if (dataBus.GetData<User>(Constants.DataKeys.User) == null)
                            throw new InvalidInputException("To buy products select the user first");

                        return productsScreen.Value;
                    }
                default: return null;
            }
        }
    }
}

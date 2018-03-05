using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.ConsoleClient.Core.Exceptions;
using Shop.ConsoleClient.Core.Screens;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core
{
    public class Application : IShopApplication
    {
        private readonly IPresenter presenter;
        private IScreen currentScreen;
        private IDataBus dataBus;

        public Application(IMainScreen startScreen, IPresenter presenter, IDataBus dataBus)
        {
            this.presenter = presenter;
            this.currentScreen = startScreen;
            this.dataBus = dataBus;
        }

        private void ShowUserInfo()
        {
            User user = dataBus.GetData<User>(Constants.DataKeys.User);
            if (user != null)
            {
                presenter.WriteLine("Selected user: " + user.Name);
                presenter.WriteLine("--------------------------------\n");
            }
            else
            {
                presenter.WriteLine("Please, select user");
                presenter.WriteLine("--------------------------------\n");
            }
        }

        public void Run()
        {
            while(true)
            {
                Console.Clear();
                ShowUserInfo();
                currentScreen.Show();

                IScreen inputHandler;

                while(true)
                {
                    try
                    {
                        string pressedKey = Console.ReadLine();
                        inputHandler = currentScreen.HandleInput(pressedKey);
                        break;
                    }
                    catch (InvalidInputException ex)
                    {
                        presenter.WriteLine(ex.Message);
                        continue;
                    }                    
                }
                
                if (inputHandler == null)
                    break;
                
                currentScreen = inputHandler;
            }
            
        }
    }
}

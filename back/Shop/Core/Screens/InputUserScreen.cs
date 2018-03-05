using System;
using System.Collections.Generic;
using System.Text;
using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.Logic.Facades;
using Shop.ConsoleClient.Core.Exceptions;
using Shop.Logic.Exceptions;

namespace Shop.ConsoleClient.Core.Screens
{
    public class InputUserScreen : IInputUserScreen
    {
        private IPresenter presenter;
        private Lazy<IMainScreen> mainScreen;
        private UserFacade userFacade;
        private IDataBus dataBus;

        private const int DefaultUserBill = 10000;

        public InputUserScreen(IPresenter presenter, Func<IMainScreen> mainScreen, UserFacade userFacade, IDataBus dataBus)
        {
            this.presenter = presenter;
            this.mainScreen = new Lazy<IMainScreen>(mainScreen);
            this.userFacade = userFacade;
            this.dataBus = dataBus;
        }

        public void Show()
        {
            presenter.WriteLine("Input username or q for back");
        }

        public IScreen HandleInput(string input)
        {
            if (input == "q")
                return mainScreen.Value;

            if (string.IsNullOrEmpty(input))
                throw new InvalidInputException("Username can't be empty");

            try
            {
                var newUser = new Domain.User()
                {
                    Name = input,
                    Amount = DefaultUserBill
                };
                userFacade.Create(newUser);
                dataBus.SetData(Constants.DataKeys.User, newUser);
            }
            catch (ValidationException ex)
            {
                throw new InvalidInputException(ex.Message);                
            }            

            return mainScreen.Value;
        }
    }
}

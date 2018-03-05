using System;
using System.Collections.Generic;
using System.Text;
using Shop.ConsoleClient.Core.Contracts;
using Autofac;
using Shop.ConsoleClient.Core.Contracts.Screens;

namespace Shop.ConsoleClient.Core.Screens
{
    public class MainScreen : IMainScreen
    {
        private Lazy<ISelectUserScreen> selectUserScreen;
        private Lazy<IInputUserScreen> inputUserScreen;
        private IPresenter presenter;

        public MainScreen(Func<ISelectUserScreen> selectUserScreen, Func<IInputUserScreen> inputUserScreen, IPresenter presenter)
        {
            this.selectUserScreen = new Lazy<ISelectUserScreen>(selectUserScreen);
            this.inputUserScreen = new Lazy<IInputUserScreen>(inputUserScreen);
            this.presenter = presenter;
        }

        public void Show()
        {
            presenter.WriteLine("1. Select user");
            presenter.WriteLine("2. Create user");
            presenter.WriteLine("3. Exit");
        }

        public IScreen HandleInput(string input)
        {
            switch(input)
            {
                case "1": return selectUserScreen.Value;
                case "2": return inputUserScreen.Value;
               // case "3": return new ExitCommand();
                default: return null;
            }
        }
    }
}

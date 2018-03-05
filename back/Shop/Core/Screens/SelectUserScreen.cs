using System;
using System.Collections.Generic;
using System.Text;
using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.Contracts.Dal;
using Shop.Domain;
using System.Linq;
using Shop.ConsoleClient.Core.Exceptions;

namespace Shop.ConsoleClient.Core.Screens
{
    public class SelectUserScreen : ISelectUserScreen
    {
        private IPresenter presenter;
        private Lazy<IMainScreen> mainScreen;
        private IUsersRepository usersRepository;
        private IDataBus dataBus;

        private User[] _users;

        public SelectUserScreen(IPresenter presenter, Func<IMainScreen> mainScreen, IDataBus dataBus, IUsersRepository usersRepository)
        {
            this.presenter = presenter;
            this.mainScreen = new Lazy<IMainScreen>(mainScreen);
            this.dataBus = dataBus;
            this.usersRepository = usersRepository;
        }

        public void Show()
        {
            presenter.WriteLine("Select user or press q to move back");

            _users = usersRepository.GetAll().ToArray();
            ShowUsers();
        }

        private void ShowUsers()
        {
            int usersLength = _users.Length;
            for (int i = 0; i < usersLength; i++)
            {
                presenter.WriteLine($"{i + 1}. {_users[i].Name}");
            }            
        }

        private bool IsValidUserInput(string input, out int selectedUser)
        {
            return int.TryParse(input, out selectedUser) 
                && selectedUser >= 0 
                && selectedUser - 1 < _users.Length;
        }

        public IScreen HandleInput(string input)
        {
            if (input == "q")
                return mainScreen.Value;

            int selectedUser = -1;

            if (IsValidUserInput(input, out selectedUser))
            {
                dataBus.SetData(Constants.DataKeys.User, _users[selectedUser - 1]);
            }
            else
            {
                throw new InvalidInputException("Invalid user number");
            }

            return mainScreen.Value;

        }        
    }
}

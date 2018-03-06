using Shop.Common;
using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.ConsoleClient.Core.Exceptions;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.Logic.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core.Screens
{
    public class ProductScreen : IProductScreen
    {
        private IProductsRepository productsRepository;
        private TransactionsFacade transactionsFacade;
        private Lazy<IMainScreen> mainScreen;
        private IDataBus dataBus;
        private IPresenter presenter;
        private IUsersRepository usersRepository;

        private Product _product;

        public ProductScreen(IProductsRepository productsRepository, TransactionsFacade transactionsFacade, Func<IMainScreen> mainScreen, IDataBus dataBus, IPresenter presenter, IUsersRepository usersRepository)
        {
            this.productsRepository = productsRepository;
            this.transactionsFacade = transactionsFacade;
            this.mainScreen = new Lazy<IMainScreen>(mainScreen);
            this.dataBus = dataBus;
            this.presenter = presenter;
            this.usersRepository = usersRepository;
        }

        public IScreen HandleInput(string input)
        {
            if (input == "q")
            {
                dataBus.Remove(Constants.DataKeys.SelectedProduct);
            }
            else
            {
                int quantity = -1;
                if (!int.TryParse(input, out quantity) || quantity < 1)
                    throw new InvalidInputException("Invalid quantity");

                User currentUser = dataBus.GetData<User>(Constants.DataKeys.User)
                    .Assert(() => new InvalidOperationException("Select user"));

                transactionsFacade.Buy(currentUser.Name, _product.Id, quantity);
                dataBus.SetData(Constants.DataKeys.User, usersRepository.GetByName(currentUser.Name));
            }

            return mainScreen.Value;
        }

        public void Show()
        {
            var selectedProduct = dataBus.GetData<int?>(Constants.DataKeys.SelectedProduct);
            if (selectedProduct == null)
                throw new ScreenException("Please, select product first", mainScreen.Value);

            _product = productsRepository.GetById(selectedProduct.Value);

            presenter.WriteLine($"{_product.Name}. Price: {_product.Cost}. available quantity: {_product.Quantity}");
            presenter.WriteLine("Please type quantity to buy or type q to exit");
        }
    }
}

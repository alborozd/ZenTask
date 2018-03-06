using Shop.ConsoleClient.Core.Contracts;
using Shop.ConsoleClient.Core.Contracts.Screens;
using Shop.ConsoleClient.Core.Exceptions;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.Logic.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.ConsoleClient.Core.Screens
{
    public class ProductsListScreen : IProductsListScreen
    {
        private Lazy<IMainScreen> mainScreen;
        private Lazy<IProductScreen> productScreen;
        private IProductsRepository productsRepository;
        private IPresenter presenter;
        private IDataBus dataBus;

        private Product[] _products;

        public ProductsListScreen(IProductsRepository productsRepository, 
            IPresenter presenter, 
            Func<IMainScreen> mainScreen, 
            IDataBus dataBus, 
            Func<IProductScreen> productScreen)
        {
            this.productsRepository = productsRepository;
            this.presenter = presenter;
            this.mainScreen = new Lazy<IMainScreen>(mainScreen);
            this.dataBus = dataBus;
            this.productScreen = new Lazy<IProductScreen>(productScreen);

            dataBus.Remove(Constants.DataKeys.SelectedProduct);
        }

        public IScreen HandleInput(string input)
        {
            if (input == "q")
                return mainScreen.Value;

            int selectedProduct = -1;

            if (input.IsValidCollectionSelect(_products.Length, out selectedProduct))
            {
                int productIndex = selectedProduct - 1;                
                dataBus.SetData(Constants.DataKeys.SelectedProduct, _products[productIndex].Id);

                return productScreen.Value;
            }
            else
            {
                throw new InvalidInputException("Invalid productNumber number");
            }            
        }

        private void ShowProducts()
        {
            int len = _products.Length;
            for (int i = 0; i < len; i++)
            {
                presenter.WriteLine($"{i + 1}. {_products[i].Name}. Price: {_products[i].Cost}. Quantity: {_products[i].Quantity}");
            }
        }

        public void Show()
        {
            _products = productsRepository.GetAll().ToArray();
            presenter.WriteLine("Select product and quantity to buy or type q to exit");
            ShowProducts();
        }
    }
}

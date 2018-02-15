using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Shop.XmlDal.Tests
{

    public class ProductsRepositoryTests
    {
        private IProductsRepository repository;

        private List<Product> collectionInit = new List<Product>()
        {
            new Product() { Id = 1, Name = "Milk", Cost = 10.50m, Quantity = 150 },
            new Product() { Id = 2, Name = "Water", Cost = 5m, Quantity = 100 },
            new Product() { Id = 3, Name = "Bread", Cost = 7m, Quantity = 0 },
        };

        public ProductsRepositoryTests()
        {
            var resolver = new XmlPathResolver();
            repository = new ProductsRepository(resolver);
        }

        [Fact]
        public void ProductTests()
        {
            var actual = repository.GetAll().ToList();
            Assert.Equal(collectionInit, actual, new ProductComparer());

            //lets run tests for quantity changing
            // TODO: implement before and after tests methods logic to backup xml files before and restore after every test
            ChangeProductQuantity();
        }

        public void ChangeProductQuantity()
        {
            int productId = 2;
            int initQuantity = collectionInit.First(t => t.Id == productId).Quantity;

            int newQuantity = 50;

            repository.ChangeQuantity(productId, newQuantity);
            var product = repository.GetAll().First(t => t.Id == productId);

            Assert.Equal(newQuantity, product.Quantity);            
        }
    }
}

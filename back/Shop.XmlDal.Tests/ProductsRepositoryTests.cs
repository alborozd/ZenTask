﻿using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.XmlDal.Repositories;
using NUnit.Framework;

namespace Shop.XmlDal.Tests
{
    // TODO: implement before and after tests methods logic to backup xml files before and restore after every test. instead if using order attribute
    [TestFixture]
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

        [Test, Order(1)]
        public void ProductTests()
        {
            var actual = repository.GetAll().ToList();
            Assert.That(collectionInit, Is.EquivalentTo(actual).Using(new ProductComparer()));
        }
        
        [Test, Order(2)]
        public void ChangeProductQuantity()
        {
            //try to change quantiti of not existing product
            repository.ChangeQuantity(1000, 1);

            //next steps for existing product

            int productId = 2;
            int initQuantity = collectionInit.First(t => t.Id == productId).Quantity;

            int newQuantity = 50;

            repository.ChangeQuantity(productId, newQuantity);
            var product = repository.GetAll().First(t => t.Id == productId);

            Assert.AreEqual(newQuantity, product.Quantity);
        }

        [Test, Order(3)]
        public void GetProductById()
        {
            int productId = 3;
            var actual = repository.GetById(productId);
            var expected = collectionInit.First(t => t.Id == productId);

            Assert.IsTrue(new ProductComparer().Equals(actual, expected));
        }
    }
}

using Moq;
using NUnit.Framework;
using Shop.Common.Services;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.Logic.Facades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Environment = Shop.Common.Environment;
using Shop.Logic.Exceptions;

namespace Shop.Logic.Tests
{
    [TestFixture]
    public class TransactionsFacadeTests
    {
        private TransactionsFacade facade;

        private List<User> users = new List<User>()
            {
                new User() { Name = "Jerry", Amount = 100.5m },
                new User() { Name = "Michael", Amount = 15m },
            };

        private List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Milk", Cost = 10.50m, Quantity = 150 },
            new Product() { Id = 2, Name = "Water", Cost = 5m, Quantity = 100 },
            new Product() { Id = 3, Name = "Bread", Cost = 7m, Quantity = 0 },
        };

        List<Discount> discounts = new List<Discount>()
        {
            new Discount() { ProductId = 1, StartDate = DateTime.Parse("2017-06-08"), Percents = 10 },
            new Discount() { ProductId = 1, StartDate = DateTime.Parse("2018-01-01"), Percents = 5 },
        };

        private string UserNameToChangeBalance = "";
        private decimal NewUserAmount = -1;

        private int ProductidToChangeQuantity = -1;
        private int NewProductQuantity = -1;

        private Transaction AddedTransaction = null;

        [SetUp]
        public void Init()
        {
            UserNameToChangeBalance = "";
            NewUserAmount = -1;

            ProductidToChangeQuantity = -1;
            NewProductQuantity = -1;

            AddedTransaction = null;
        }

        public TransactionsFacadeTests()
        {
            var lockService = new MutextLock();
            var environment = new Environment();

            var usersRepoMock = new Mock<IUsersRepository>();
            usersRepoMock
                .Setup(t => t.GetByName(It.IsAny<string>()))
                .Returns((string name) => users.First(t => t.Name == name));

            usersRepoMock
                .Setup(t => t.ChangeBalance(It.IsAny<string>(), It.IsAny<decimal>()))
                .Callback((string username, decimal newBalance) =>
                {
                    UserNameToChangeBalance = username;
                    NewUserAmount = newBalance;
                });

            var discountsRepo = new Mock<IDiscountsRepository>();
            discountsRepo
                .Setup(t => t.GetDiscountsByProduct(It.IsAny<int>()))
                .Returns((int productId) => discounts.Where(t => t.ProductId == productId));

            var productsRepo = new Mock<IProductsRepository>();
            productsRepo
                .Setup(t => t.GetById(It.IsAny<int>()))
                .Returns((int productId) => products.FirstOrDefault(t => t.Id == productId));

            productsRepo
                .Setup(t => t.ChangeQuantity(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int productId, int quantity) =>
                {
                    ProductidToChangeQuantity = productId;
                    NewProductQuantity = quantity;
                });

            var errorsRepoMock = new Mock<ITransactionErrorsRepository>();

            var transactionsRepoMock = new Mock<ITransactionsRepository>();
            transactionsRepoMock
                .Setup(t => t.Add(It.IsAny<Transaction>()))
                .Callback((Transaction transaction) => AddedTransaction = transaction);

            facade = new TransactionsFacade(usersRepoMock.Object,
                productsRepo.Object,
                discountsRepo.Object,
                environment,
                lockService,
                errorsRepoMock.Object,
                transactionsRepoMock.Object);
        }

        [Test]
        public void WrongQuantityValue()
        {
            Assert.Throws(typeof(InvalidOperationException), () => facade.Buy("Jerry", 1, -5));
            Assert.Throws(typeof(InvalidOperationException), () => facade.Buy("Jerry", 1, 0));
        }

        [Test]
        public void ProductAndUserNotEixt()
        {
            Assert.Throws(typeof(InvalidOperationException), () => facade.Buy("User name", 1000, 100));
            Assert.Throws(typeof(InvalidOperationException), () => facade.Buy("Jerry", 1000, 100));
        }

        [Test]
        public void NotEnoughProductToBuy()
        {
            Assert.Throws(typeof(ValidationException), () => facade.Buy("Jerry", 3, 1));
            Assert.Throws(typeof(ValidationException), () => facade.Buy("Jerry", 2, 1000));
        }

        [Test]
        public void NotEnoughMoneyToBuy()
        {
            Assert.Throws(typeof(ValidationException), () => facade.Buy("Jerry", 1, 10000));
        }

        [Test]
        public void BuyWithDiscount()
        {
            string user = "Jerry";
            int productId = 1;
            int quantity = 5;
            var expectedTransaction = new Transaction()
            {
                Username = user,
                ProductId = productId,
                ProductName = "Milk",
                Quantity = quantity,
                Cost = 10.50M,
                Discount = 5,
                SellCost = 9.975M,
                Amount = 49.875M
            };

            facade.Buy(user, productId, quantity);

            Assert.IsTrue(new TransactionComparer().Equals(expectedTransaction, AddedTransaction));

            Assert.AreEqual(UserNameToChangeBalance, user);
            Assert.AreEqual(NewUserAmount, 50.625M);

            Assert.AreEqual(ProductidToChangeQuantity, productId);
            Assert.AreEqual(NewProductQuantity, 145);
        }

        [Test]
        public void BuyWithoutDiscount()
        {
            string user = "Michael";
            int productId = 2;
            int quantity = 2;
            var expectedTransaction = new Transaction()
            {
                Username = user,
                ProductId = productId,
                ProductName = "Water",
                Quantity = quantity,
                Cost = 5M,
                Discount = 0,
                SellCost = 5M,
                Amount = 10M
            };

            facade.Buy(user, productId, quantity);

            Assert.IsTrue(new TransactionComparer().Equals(expectedTransaction, AddedTransaction));

            Assert.AreEqual(UserNameToChangeBalance, user);
            Assert.AreEqual(NewUserAmount, 5M);

            Assert.AreEqual(ProductidToChangeQuantity, productId);
            Assert.AreEqual(NewProductQuantity, 98);
        }
    }
}

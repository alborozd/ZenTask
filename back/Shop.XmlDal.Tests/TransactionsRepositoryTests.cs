using System.Collections.Generic;
using System.Linq;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Repositories;
using Xunit;

namespace Shop.XmlDal.Tests
{
    public class TransactionsRepositoryTests
    {
        private ITransactionsRepository repository;

        private List<Transaction> collectionInit = new List<Transaction>()
        {
            new Transaction() { Username = "Jerry", ProductId = 1, ProductName = "Milk", Quantity = 5, Cost = 10.50M, Discount = 5f, SellCost = 9.975M, Amount = 49.875M},
            new Transaction() { Username = "Jerry", ProductId = 2, ProductName = "Water", Quantity = 1, Cost = 5, Discount = 1, SellCost = 4.95M, Amount = 4.95M},
            new Transaction() { Username = "John Lennon", ProductId = 3, ProductName = "Bread", Quantity = 3, Cost = 7, Discount = 0, SellCost = 7, Amount = 21}
        };
        
        public TransactionsRepositoryTests()
        {
            var resolver = new XmlPathResolver();
            repository = new TransactionsRepository(resolver);
        }

        [Fact]
        public void GetAlltransactionsTest()
        {
            var actual = repository.GetAll().ToList();
            Assert.Equal(collectionInit, actual, new TransactionComparer());
        }

        [Fact]
        public void AddTransactionTest()
        {
            var transactionToAdd = new Transaction()
            {
                Username = "Tommy",
                ProductId = 1,
                ProductName = "Milk",
                Amount = 10,
                Cost = 4.55M,
                Discount = 2.34f,
                Quantity = 1,
                SellCost = 14.87M
            };

            repository.Add(transactionToAdd);
            var expected = collectionInit.ToList();
            expected.Add(transactionToAdd);

            var actual = repository.GetAll().ToList();
            Assert.Equal(expected, actual);
        }
    }
}
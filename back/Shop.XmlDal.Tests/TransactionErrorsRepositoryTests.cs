using Shop.Contracts.Dal;
using Shop.XmlDal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Shop.Domain;
using System.Globalization;
using NUnit.Framework;

namespace Shop.XmlDal.Tests
{
    [TestFixture]
    public class TransactionErrorsRepositoryTests
    {
        private ITransactionErrorsRepository repository;

        private List<TransactionError> collectionInit = new List<TransactionError>()
        {
            new TransactionError() { Date = DateTime.Parse("2017-10-05", CultureInfo.InvariantCulture), Description = "Invalid operation for current user" },
            new TransactionError() { Date = DateTime.Parse("2017-10-05", CultureInfo.InvariantCulture), Description = "Not enough money to buy the product" }
        };

        public TransactionErrorsRepositoryTests()
        {
            var resolver = new XmlPathResolver();
            repository = new TransacionErrorsRepository(resolver);
        }

        [Test, Order(1)]
        public void GetAllErrorsTest()
        {
            var actual = repository.GetAll().ToList();
            Assert.That(collectionInit, Is.EquivalentTo(actual).Using(new TransactionErrorComparer()));
        }

        [Test, Order(2)]
        public void AddNewError()
        {
            var error = new TransactionError()
            {
                Date = DateTime.Parse("2018-08-11"),
                Description = "Some new transaction error"
            };

            repository.Add(error);
            var expected = collectionInit.ToList();
            expected.Add(error);

            var actual = repository.GetAll().ToList();
            Assert.That(expected, Is.EquivalentTo(actual).Using(new TransactionErrorComparer()));
        }
    }
}

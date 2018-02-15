using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Shop.XmlDal.Tests
{
    public class DiscountsRepositoryTests
    {
        private IDiscountsRepository repository;

        List<Discount> collectionInit = new List<Discount>()
        {
            new Discount() { ProductId = 1, StartDate = DateTime.Parse("2017-06-08"), Percents = 10 },
            new Discount() { ProductId = 1, StartDate = DateTime.Parse("2017-10-25"), Percents = 10.5f },
            new Discount() { ProductId = 1, StartDate = DateTime.Parse("2018-01-01"), Percents = 5 },
            new Discount() { ProductId = 2, StartDate = DateTime.Parse("2018-01-01"), Percents = 1 },
        };
        
        public DiscountsRepositoryTests()
        {
            var resolver = new XmlPathResolver();
            repository = new DiscountsRepository(resolver);
        }

        [Fact]
        public void GetAllDiscountsTest()
        {
            var discounts = repository.GetAll().ToList();
            Assert.Equal(collectionInit, discounts, new DiscountComparer());
        }

        [Fact]
        public void GetDiscountsByProductId()
        {
            int productId = 1;
            var actual = repository.GetDiscountsByProduct(productId).ToList();
            var expected = collectionInit.Where(t => t.ProductId == productId).ToList();

            Assert.Equal(expected, actual, new DiscountComparer());
        }
    }
}

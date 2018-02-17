using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Shop.XmlDal.Repositories;
using NUnit.Framework;

namespace Shop.XmlDal.Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        private IUsersRepository repository;

        private List<User> collectionInit = new List<User>()
            {
                new User() { Name = "Jerry", Amount = 100.5m },
                new User() { Name = "Michael", Amount = 15m },
                new User() { Name = "John Lennon", Amount = 0m },
                new User() { Name = "George", Amount = 0m },
            };

        public UsersRepositoryTests()
        {
            var pathResolver = new XmlPathResolver();
            repository = new UsersRepository(pathResolver);
        }

        [Test, Order(1)]
        public void GetAllUsersTest()
        {
            var actual = repository.GetAll().ToList();
            Assert.That(collectionInit, Is.EquivalentTo(actual).Using(new UserComparer()));
        }

        [Test, Order(2)]
        public void AddUserTest()
        {
            var user = new User();
            user.Name = "Alexander";
            user.Amount = 200;

            repository.Add(user);

            var expected = collectionInit.ToList();
            expected.Add(user);

            var actual = repository.GetAll().ToList();
            Assert.That(expected, Is.EquivalentTo(actual).Using(new UserComparer()));
        }

        [Test, Order(3)]
        public void SearchUserByName()
        {
            //lets find user that exists in xml
            string search = "John Lennon";
            var user = repository.GetByName(search);
            Assert.NotNull(user);

            var expected = collectionInit.First(t => t.Name == search);
            Assert.True(new UserComparer().Equals(expected, user));

            //lets find user tha doesn't exist in xml
            var nullUser = repository.GetByName("Some strange name");
            Assert.Null(nullUser);
        }
    }
}

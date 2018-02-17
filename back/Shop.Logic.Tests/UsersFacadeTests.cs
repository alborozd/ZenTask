using Moq;
using NUnit.Framework;
using Shop.Common.Services;
using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.Logic.Exceptions;
using Shop.Logic.Facades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Logic.Tests
{
    [TestFixture]
    public class UsersFacadeTests
    {
        private UserFacade userFacade;

        private List<User> users = new List<User>()
            {
                new User() { Name = "Jerry", Amount = 100.5m },
                new User() { Name = "Michael", Amount = 15m },
            };

        private User AddedUser = null;

        public UsersFacadeTests()
        {
            var lockService = new MutextLock();
            Mock<IUsersRepository> repositoryMock = new Mock<IUsersRepository>();
            repositoryMock
                .Setup(t => t.GetByName(It.IsAny<string>()))
                .Returns((string name) 
                    => users.FirstOrDefault(t => t.Name == name));

            repositoryMock
                .Setup(t => t.Add(It.IsAny<User>()))
                .Callback((User addedUser) => AddedUser = addedUser);

            userFacade = new UserFacade(repositoryMock.Object, lockService);
        }

        [Test]
        public void CreateNonExistingUser()
        {
            AddedUser = null;

            var newUser = new User()
            {
                Name = "Michael Jackson",
                Amount = 10000M
            };

            userFacade.Create(newUser);
            Assert.IsTrue(new UserComparer().Equals(newUser, AddedUser));
        }

        [Test]
        public void CreateUserThatExists()
        {
            var user = new User() { Name = "Jerry", Amount = 50M };
            Assert.Throws(typeof(ValidationException), () => userFacade.Create(user));
        }
    }
}

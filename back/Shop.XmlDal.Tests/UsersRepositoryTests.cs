using Shop.Domain;
using System;
using Xunit;

namespace Shop.XmlDal.Tests
{
    public class UsersRepositoryTests
    {

        [Fact]
        public void Test1()
        {
            var pathResolver = new XmlPathResolver();
            UsersRepository repository = new UsersRepository(pathResolver);

            //repository.Add(new User()
            //{
            //    Name = "Jerry",
            //    Amount = 100.5M
            //});

            //var users = repository.GetAll();
            var user = repository.GetByName("Jerry");
            var user1 = repository.GetByName("Jerry1");
        }
    }
}

using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IUsersRepository : IRepository<User>
    {
        User GetByName(string name);
    }
}

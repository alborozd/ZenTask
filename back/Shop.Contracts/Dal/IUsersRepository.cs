﻿using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        User GetByName(string name);
        void ChangeBalance(string username, decimal newBalance);
    }
}

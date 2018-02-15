using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IRepository
    {
    }

    public interface IReadRepository<T> : IRepository 
        where T : Entity
    {
        IEnumerable<T> GetAll();
    }
}

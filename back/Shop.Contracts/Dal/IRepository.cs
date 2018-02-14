using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IRepository
    {
    }

    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T Add();
    }
}

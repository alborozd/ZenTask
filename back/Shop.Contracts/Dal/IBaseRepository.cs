using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IBaseRepository<T> : IReadRepository<T>
        where T : Entity
    {
        T Add(T entity);
    }
}

using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;

namespace Shop.XmlDal
{
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected abstract string FileName { get; }

        public T Add()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

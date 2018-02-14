using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.XmlDal
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        protected override string FileName => throw new NotImplementedException();
    }
}

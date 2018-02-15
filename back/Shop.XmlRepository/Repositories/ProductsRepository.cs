using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop.XmlDal
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        public ProductsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => throw new NotImplementedException();

        protected override Product GetEntity(XmlElement node)
        {
            throw new NotImplementedException();
        }

        protected override XmlElement SetEntity(Product entity, XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}

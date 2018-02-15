using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop.XmlDal
{
    public class DiscountsRepository : BaseRepository<Discount>, IDiscountsRepository
    {
        public DiscountsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => throw new NotImplementedException();

        protected override Discount GetEntity(XmlElement node)
        {
            throw new NotImplementedException();
        }

        protected override XmlElement SetEntity(Discount entity, XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}

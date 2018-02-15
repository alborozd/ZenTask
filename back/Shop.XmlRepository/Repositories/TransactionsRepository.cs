using Shop.Contracts.Dal;
using Shop.Domain;
using Shop.XmlDal.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Shop.XmlDal
{
    public class TransactionsRepository : BaseRepository<Transaction>, ITransactionsRepository
    {
        public TransactionsRepository(IXmlPathResolver resolver)
            : base(resolver) { }

        protected override string FileName => throw new NotImplementedException();

        protected override Transaction GetEntity(XmlElement node)
        {
            throw new NotImplementedException();
        }

        protected override XmlElement SetEntity(Transaction entity, XmlDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}

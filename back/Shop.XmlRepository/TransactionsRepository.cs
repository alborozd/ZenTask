using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.XmlDal
{
    public class TransactionsRepository : BaseRepository<Transaction>, ITransactionsRepository
    {
        protected override string FileName => throw new NotImplementedException();
    }
}

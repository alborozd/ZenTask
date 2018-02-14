using Shop.Contracts.Dal;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.XmlDal
{
    public class DiscountsRepository : BaseRepository<Discount>, IDiscountsRepository
    {
        protected override string FileName => throw new NotImplementedException();
    }
}

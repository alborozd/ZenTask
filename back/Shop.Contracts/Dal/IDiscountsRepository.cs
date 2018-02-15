using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IDiscountsRepository : IReadRepository<Discount>
    {
        IEnumerable<Discount> GetDiscountsByProduct(int productId);
    }
}

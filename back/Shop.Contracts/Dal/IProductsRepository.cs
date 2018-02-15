using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Contracts.Dal
{
    public interface IProductsRepository : IReadRepository<Product>
    {
        void ChangeQuantity(int productId, int newQuantity);
    }
}

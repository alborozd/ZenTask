using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain
{
    public class Product : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (y == null && x == null)
                return true;

            if (x == null | y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Quantity == y.Quantity
                && x.Cost == y.Cost;
        }

        public int GetHashCode(Product obj)
        {
            int hash = 27;
            hash = (13 * hash) + obj.Id.GetHashCode();
            hash = (13 * hash) + obj.Name.GetHashCode();
            hash = (13 * hash) + obj.Quantity.GetHashCode();
            hash = (13 * hash) + obj.Cost.GetHashCode();

            return hash;
        }
    }
}

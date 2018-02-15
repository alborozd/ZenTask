using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain
{
    public class Discount : Entity
    {
        public int ProductId { get; set; }
        public DateTime? StartDate { get; set; }
        public float Percents { get; set; }
    }

    public class DiscountComparer : IEqualityComparer<Discount>
    {
        public bool Equals(Discount x, Discount y)
        {
            if (y == null && x == null)
                return true;

            if (x == null | y == null)
                return false;

            return x.ProductId == y.ProductId
                && x.Percents == y.Percents
                && x.StartDate == y.StartDate;
        }

        public int GetHashCode(Discount obj)
        {
            int hash = 27;
            hash = (13 * hash) + obj.Percents.GetHashCode();
            hash = (13 * hash) + obj.ProductId.GetHashCode();
            hash = (13 * hash) + obj.StartDate.GetHashCode();

            return hash;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain
{
    public class TransactionError : Entity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class TransactionErrorComparer : IEqualityComparer<TransactionError>
    {
        public bool Equals(TransactionError x, TransactionError y)
        {
            if (y == null && x == null)
                return true;

            if (x == null | y == null)
                return false;

            return x.Date == y.Date
                && x.Description == y.Description;
        }

        public int GetHashCode(TransactionError obj)
        {
            int hash = 27;
            hash = (13 * hash) + obj.Date.GetHashCode();
            hash = (13 * hash) + obj.Description.GetHashCode();
            
            return hash;
        }
    }
}

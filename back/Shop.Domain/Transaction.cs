using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain
{
    public class Transaction : Entity
    {
        public string Username { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public float Discount { get; set; }
        public decimal SellCost { get; set; }
        public decimal Amount { get; set; }
    }

    public class TransactionComparer : IEqualityComparer<Transaction>
    {
        public bool Equals(Transaction x, Transaction y)
        {
            if (y == null && x == null)
                return true;

            if (x == null | y == null)
                return false;

            return x.Username == y.Username
                   && x.ProductId == y.ProductId
                   && x.ProductName == y.ProductName
                   && x.Quantity == y.Quantity
                   && x.Cost == y.Cost
                   && x.Discount == y.Discount
                   && x.SellCost == y.SellCost
                   && x.Amount == y.Amount;
        }

        public int GetHashCode(Transaction obj)
        {
            int hash = 27;
            hash = (13 * hash) + obj.Username.GetHashCode();
            hash = (13 * hash) + obj.ProductId.GetHashCode();
            hash = (13 * hash) + obj.ProductName.GetHashCode();
            hash = (13 * hash) + obj.Quantity.GetHashCode();
            hash = (13 * hash) + obj.Cost.GetHashCode();
            hash = (13 * hash) + obj.Discount.GetHashCode();
            hash = (13 * hash) + obj.SellCost.GetHashCode();
            hash = (13 * hash) + obj.Amount.GetHashCode();

            return hash;
        }
    }
}
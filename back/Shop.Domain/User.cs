using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain
{
    public class User : Entity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }

    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (y == null && x == null)
                return true;

            if (x == null | y == null)
                return false;

            return x.Name == y.Name
                && y.Amount == y.Amount;
        }

        public int GetHashCode(User obj)
        {
            int hash = 27;
            hash = (13 * hash) + obj.Amount.GetHashCode();
            hash = (13 * hash) + obj.Name.GetHashCode();

            return hash;
        }
    }
}

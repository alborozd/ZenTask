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
}

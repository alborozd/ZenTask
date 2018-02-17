using Shop.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common
{
    public class Environment : IEnvironment
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}

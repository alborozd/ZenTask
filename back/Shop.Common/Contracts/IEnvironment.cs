using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Contracts
{
    public interface IEnvironment
    {
        DateTime GetUtcNow();
    }
}

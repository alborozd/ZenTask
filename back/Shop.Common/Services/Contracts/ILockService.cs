using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Services.Contracts
{
    public interface ILockService
    {
        void Execute(Action action);
    }
}

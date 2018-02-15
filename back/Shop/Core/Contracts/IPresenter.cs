using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core.Contracts
{
    public interface IPresenter
    {
        void Write(string message);
        void WriteLine(string message);
    }
}

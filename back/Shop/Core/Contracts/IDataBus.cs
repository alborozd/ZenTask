using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Core.Contracts
{
    public interface IDataBus
    {
        void SetData<T>(string key, T data);
        T GetData<T>(string key);
        void Remove(string key);
    }
}

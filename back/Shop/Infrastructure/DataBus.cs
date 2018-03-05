using Shop.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.ConsoleClient.Infrastructure
{
    public class DataBus : IDataBus
    {
        private static Dictionary<string, object> _data = new Dictionary<string, object>();

        public T GetData<T>(string key)
        {
            if (_data.ContainsKey(key))
                return (T)_data[key];

            return default(T);
        }

        public void Remove(string key)
        {
            if (_data.ContainsKey(key))
                _data.Remove(key);
        }

        public void SetData<T>(string key, T data)
        {
            if (_data.ContainsKey(key))
                _data.Remove(key);

            _data.Add(key, data);
        }
    }
}

using System.Collections.Generic;

namespace IOCInfrastructure
{
    public class ServiceTransactionData : IServiceTransactionData
    {
        private Dictionary<string, object> _data;

        public ServiceTransactionData()
        {
            _data = new Dictionary<string, object>();
        }

        public object Get(string key)
        {
            if (_data.ContainsKey(key))
            {
                return _data[key];
            }
            else
            {
                return null;
            }
        }

        public void Set(string key, object data)
        {
            _data[key] = data;
        }

        public T Get<T>(string key)
        {
            if (_data.ContainsKey(key))
            {
                var d = _data[key];
                return (T)d;
            }
            else
            {
                return default(T);
            }
        }
    }
}
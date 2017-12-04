using System;

namespace IOCInfrastructure
{
    public interface IServiceTransactionData
    {
        Object Get(string key);

        T Get<T>(string key);

        void Set(string key, Object data);
     
    }
}
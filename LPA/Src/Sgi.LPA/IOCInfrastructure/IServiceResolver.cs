using System;

namespace IOCInfrastructure
{
    public interface IServiceResolver
    {
        T GetInstance<T>() where T : class;

        T GetInstance<T>(string key) where T : class;

        object GetInstance(Type type);
    }
}
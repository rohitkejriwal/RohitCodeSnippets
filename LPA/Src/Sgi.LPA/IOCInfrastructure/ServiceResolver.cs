using Microsoft.Practices.Unity;
using System;

namespace IOCInfrastructure
{
    public class ServiceResolver : IServiceResolver
    {
        private UnityContainer _container;

        public ServiceResolver(UnityContainer _container)
        {
            this._container = _container;
        }

        public T GetInstance<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public T GetInstance<T>(string key) where T : class
        {
            return _container.Resolve<T>(key);
        }

        public object GetInstance(Type type)
        {
            return _container.Resolve(type, string.Empty);
        }
    }
}
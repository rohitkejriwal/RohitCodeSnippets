using Logger.Core;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace IOCInfrastructure
{
    public class ServiceContainer : IServiceRegister, IDisposable
    {
        protected UnityContainer _container;

        private Dictionary<ServiceLifetime, Func<LifetimeManager>> _lifetimeFactory;

        public ServiceContainer()
        {
            _container = new UnityContainer();
            LoadLifeTimeFactory();
            RegisterDefaultDependency();
        }

        protected virtual void RegisterDefaultDependency()
        {
            var serviceResolver = new ServiceResolver(_container);
            RegisterInstance<IServiceResolver>(serviceResolver, ServiceLifetime.Single);
            Register<ILogger, Logger.Logger>(ServiceLifetime.Single);
        }

        public void Register<TF, TT>() where TT : TF
        {
            _container.RegisterType<TF, TT>();
        }

        public void Register<TF, TT>(string key) where TT : TF
        {
            _container.RegisterType<TF, TT>(key);
        }

        public void Register(Type typeSource, Type typeDestination)
        {
            _container.RegisterType(typeSource, typeDestination);
        }

        public void RegisterInstance<TF>(TF obj, ServiceLifetime lifetime)
        {
            _container.RegisterInstance<TF>(obj, GetLifetime(lifetime));
        }

        public void RegisterInstance<TF>(TF obj, string name)
        {
            _container.RegisterInstance<TF>(name, obj, GetLifetime(ServiceLifetime.Single));
        }

        public void RegisterInstance<TF>(TF obj, string name, ServiceLifetime lifetime)
        {
            _container.RegisterInstance<TF>(name, obj, GetLifetime(lifetime));
        }

        public void RegisterInstance<TF>(Dictionary<string, TF> obj, ServiceLifetime lifetime, IResolverConfig resolverconfig)
        {
            foreach (var item in obj)
            {
                _container.RegisterInstance<TF>(item.Key, item.Value, GetLifetime(lifetime));
            }
        }

        public void Register<TF, TT>(ServiceLifetime lifetime) where TT : TF
        {
            _container.RegisterType<TF, TT>(GetLifetime(lifetime));
        }

        public IServiceResolver GetResolver()
        {
            return _container.Resolve<IServiceResolver>();
        }

        private LifetimeManager GetLifetime(ServiceLifetime lifetime)
        {
            return _lifetimeFactory[lifetime]();
        }

        private void LoadLifeTimeFactory()
        {
            _lifetimeFactory = new Dictionary<ServiceLifetime, Func<LifetimeManager>>();
            _lifetimeFactory[ServiceLifetime.PerRequest] = PerRequestLifetime;
            _lifetimeFactory[ServiceLifetime.Single] = SingleLifetime;
        }

        protected virtual LifetimeManager PerRequestLifetime()
        {
            throw new NotImplementedException("ServiceLifetime.PerRequest is only for http request");
        }

        private LifetimeManager SingleLifetime()
        {
            return new ContainerControlledLifetimeManager();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _container != null)
            {
                _container.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Register<TF, TT>(InjectionConstructor constructor) where TT : TF
        {
            _container.RegisterType<TF, TT>(constructor);
        }

        public void Register<TF, TT>(string name, InjectionConstructor constructor) where TT : TF
        {
            _container.RegisterType<TF, TT>(name, constructor);
        }

        public void Register<TF, TT>(InjectionConstructor constructor, ServiceLifetime lifetime) where TT : TF
        {
            _container.RegisterType<TF, TT>(GetLifetime(lifetime), constructor);
        }
    }
}
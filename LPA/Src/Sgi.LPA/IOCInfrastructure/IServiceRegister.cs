using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace IOCInfrastructure
{
    public interface IServiceRegister
    {
        void Register<TF, TT>() where TT : TF;

        void Register<TF, TT>(string key) where TT : TF;

        void Register<TF, TT>(InjectionConstructor constructor) where TT : TF;
        
        void Register<TF, TT>(string name, InjectionConstructor constructor) where TT : TF;

        void Register<TF, TT>(InjectionConstructor constructor, ServiceLifetime lifetime) where TT : TF;

        void Register<TF, TT>(ServiceLifetime lifetime) where TT : TF;

        void RegisterInstance<TF>(TF obj, ServiceLifetime lifetime);

        void RegisterInstance<TF>(TF obj,string name);

        void RegisterInstance<TF>(TF obj,string name,ServiceLifetime lifetime);

        void RegisterInstance<TF>(Dictionary<string, TF> obj, ServiceLifetime lifetime, IResolverConfig resolverconfig);

        void Register(Type typeSource, Type typeDestination);

        IServiceResolver GetResolver();
    }
}
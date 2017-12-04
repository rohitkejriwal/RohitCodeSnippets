using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace IOCInfrastructure.MVC
{
    public class MVCServiceContainer : ServiceContainer, IMVCDependencyResolver
    {
        protected override void RegisterDefaultDependency()
        {
            base.RegisterDefaultDependency();
            Register<IServiceTransactionData, ServiceTransactionData>(ServiceLifetime.PerRequest);
            Register<ITransactionTracker, TransactionTracker>();
        }

        protected override Microsoft.Practices.Unity.LifetimeManager PerRequestLifetime()
        {
            return new PerRequestLifetimeManager();
        }

        public System.Web.Mvc.IDependencyResolver GetMVCDependencyResolver()
        {
            return new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(_container);
        }

        public System.Web.Mvc.FilterAttributeFilterProvider GetAttributeFilterProvider()
        {
            return new UnityFilterAttributeFilterProvider(_container);
        }

        public System.Web.Http.Dependencies.IDependencyResolver GetWebApiDependencyResolver()
        {
            return new Unity.WebApi.UnityDependencyResolver(_container);
        }
    }
}
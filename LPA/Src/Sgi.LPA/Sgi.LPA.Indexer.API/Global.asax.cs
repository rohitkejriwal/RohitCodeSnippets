using IOCInfrastructure;
using IOCInfrastructure.MVC;
using Sgi.LPA.Indexer.API.App_Start;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sgi.LPA.Indexer.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IContainerProviderAccessor
    {
        public static IServiceRegister _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var container = new MVCServiceContainer();
            MvcApplication._container = container;
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());

            FilterProviders.Providers.Add(container.GetAttributeFilterProvider());
            DependencyResolver.SetResolver(container.GetMVCDependencyResolver());
            GlobalConfiguration.Configuration.DependencyResolver = container.GetWebApiDependencyResolver();

            new Dependency().AddDepenency(_container);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public IOCInfrastructure.IServiceResolver ServiceResolver
        {
            get { return _container.GetResolver(); }
        }
    }
}
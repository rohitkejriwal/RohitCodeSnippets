using IOCInfrastructure;
using SGI.LPA.Crawler.Service;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Sgi.MongoDBService.Interfaces;
using Logger.LogWriter.Mongo;
using Logger.Core;
using SGI.LPA.Crawler.App_Start;
using Sgi.Core.DBService;
using Sgi.MongoDBService;
using Microsoft.Practices.Unity;
using IOCInfrastructure.MVC;

namespace SGI.LPA.Crawler
{
    public class WebApiApplication : System.Web.HttpApplication, IContainerProviderAccessor
    {
        public static IServiceRegister _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var container = new MVCServiceContainer();
            WebApiApplication._container = container;

            //FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());

            FilterProviders.Providers.Add(container.GetAttributeFilterProvider());
            DependencyResolver.SetResolver(container.GetMVCDependencyResolver());
            GlobalConfiguration.Configuration.DependencyResolver = container.GetWebApiDependencyResolver();

            #region Dependency Injection Using IOC Infrastructure
            //_container = new MVCServiceContainer();
            var config = new MongoLogConfig();
            _container.RegisterInstance<IMongoLogConfig>(config, ServiceLifetime.Single);
            _container.Register<ILogWriter, MongoLogWriter>(ServiceLifetime.Single);
            _container.RegisterInstance<ILogConfig>(config, ServiceLifetime.Single);
            _container.Register<ILogHelper, CrawlerLogHelper>();

            _container.RegisterInstance<IMongoDBConfig>(new MongoDbWebCrawlerConfig(), "WebCrawlerConfig");
            _container.Register<IDBRepository, MongoRepository>("webCrawlerDbRepository", new InjectionConstructor(
                new ResolvedParameter<IMongoDBConfig>("WebCrawlerConfig")));

            _container.Register<ICrawlerService,WebCrawlerService>("WebCrawlerService");
            #endregion

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public IServiceResolver ServiceResolver
        {
            get { return _container.GetResolver(); }
        }
    }
}

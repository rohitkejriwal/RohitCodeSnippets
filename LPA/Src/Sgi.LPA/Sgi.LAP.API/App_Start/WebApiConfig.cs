using IOCInfrastructure.MVC;
using System.Web.Http;

namespace Sgi.LAP.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new CustomExceptionFilter());
            config.Services.Add(typeof(System.Web.Http.Filters.IFilterProvider), new IOCLogFilterProvider(MvcApplication._container.GetResolver()));
        }
    }
}
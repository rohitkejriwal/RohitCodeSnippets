using IOCInfrastructure.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SGI.LPA.Crawler
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Add(typeof(System.Web.Http.Filters.IFilterProvider), new IOCLogFilterProvider(WebApiApplication._container.GetResolver()));
        }
    }
}

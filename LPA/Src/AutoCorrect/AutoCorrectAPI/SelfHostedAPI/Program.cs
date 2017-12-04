using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace AutoCorrectAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Server started ....");
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}

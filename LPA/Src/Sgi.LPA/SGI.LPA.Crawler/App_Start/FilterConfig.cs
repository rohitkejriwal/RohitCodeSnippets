using System.Web;
using System.Web.Mvc;

namespace SGI.LPA.Crawler
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

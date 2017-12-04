using System.Web.Mvc;

namespace Sgi.LPA.ChatApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System.Web.Mvc;
using Web.Attributes;

namespace Web.Controllers
{
    [RequireLogin]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
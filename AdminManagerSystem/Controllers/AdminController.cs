using System.Web.Mvc;

namespace AdminManagerSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET
        public ActionResult Index()
        {
            ViewBag.user = "admin";
            ViewBag.index = "active";
            return View();
        }
        public ActionResult Request()
        {
            ViewBag.user = "admin";
            ViewBag.request = "active";
            return View();
        }
        public ActionResult RegisterModerator()
        {
            ViewBag.user = "admin";
            ViewBag.register_moderator = "active";
            return View();
        }
    }
}
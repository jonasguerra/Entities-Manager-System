using System.Web.Mvc;

namespace EntitiesManagerSystem.Controllers
{
    public class VoluntaryController : Controller
    {
        // GET
        public ActionResult Index()
        {
            ViewBag.user = "voluntary";
            ViewBag.index = "active";
            
            return View();
        }
        public ActionResult Event()
        {
            ViewBag.user = "voluntary";
            ViewBag.events = "active";
            
            return View();
        }
        public ActionResult Donate()
        {
            ViewBag.user = "voluntary";
            ViewBag.donate = "active";
            
            return View();
        }
    }
}
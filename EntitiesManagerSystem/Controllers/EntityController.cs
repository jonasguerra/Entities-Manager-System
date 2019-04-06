using System.Web.Mvc;

namespace EntitiesManagerSystem.Controllers
{
    public class EntityController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.user = "entity";
            ViewBag.index = "active";
            
            return View();
        }
        
        public ActionResult RegisterEvent()
        {

            ViewBag.user = "entity";
            ViewBag.register_event = "active";
            
            return View();
        }
    }
}
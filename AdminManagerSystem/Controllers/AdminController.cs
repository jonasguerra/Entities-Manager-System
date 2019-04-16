using System.Web.Mvc;
using AdminManagerSystem.Models;

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
        public ActionResult Register()
        {
            ViewBag.user = "admin";
            ViewBag.register = "active";
            return View();
        }
        
        
        
        //###################
        //### POST METHOD ###
        //###################
        
        [HttpPost]
        public ActionResult SaveModerator(Moderator moderator)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            ViewBag.user = "admin";
            ViewBag.register = "active";
            ViewBag.save_moderator_error = "true";
            return View("Register",moderator);
        }
    }
}
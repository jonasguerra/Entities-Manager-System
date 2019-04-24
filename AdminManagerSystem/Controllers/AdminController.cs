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
        
        //######### AJAX AFFINITY ##########
        
        [HttpPost]
        public ActionResult TrashAffinity()
        {
            return Json(new {status="success", message_title="Afinidade excluida com sucesso"});
        }
        
        [HttpPost]
        public ActionResult EditAffinity()
        {
            return Json(new {status="success", message_title="Afinidade editada com sucesso"});
        }
        
        [HttpPost]
        public ActionResult ApproveAffinity()
        {
            return Json(new {status="success", message_title="Afinidade aprovada com sucesso"});
        }
        
        //######### AJAX VOLUNTARY ##########
        
        [HttpPost]
        public ActionResult TrashVoluntary()
        {
            return Json(new {status="success", message_title="Voluntário excluido com sucesso"});
        }
        
        [HttpPost]
        public ActionResult ShowMoreVoluntary()
        {
            return Json(new {status="success"});
        }
        
        [HttpPost]
        public ActionResult ApproveVoluntary()
        {
            return Json(new {status="success", message_title="Voluntário aprovado com sucesso"});
        }
        
        //######### AJAX ENTITY ##########
        
        [HttpPost]
        public ActionResult TrashEntity()
        {
            return Json(new {status="success", message_title="Entidade excluida com sucesso"});
        }
        
        [HttpPost]
        public ActionResult ShowMoreEntity()
        {
            return Json(new {status="success"});
        }
        
        [HttpPost]
        public ActionResult ApproveEntity()
        {
            return Json(new {status="success", message_title="Entidade aprovada com sucesso"});
        }
    }
}
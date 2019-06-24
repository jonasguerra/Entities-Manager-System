using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AdminManagerSystem.Consumers_API;
using AdminManagerSystem.Models;
using AdminManagerSystem.Consumers_API;
using AdminManagerSystem.Models.Voluntary;

namespace AdminManagerSystem.Controllers
{
    public class AdminController : Controller
    {
    
    
        private APIHttpClient clientHttp;   
        public AdminController()
        {
            clientHttp = new APIHttpClient("http://localhost:5002/api/");
        }
    
    
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
            
            var volunteers = clientHttp.Get<List<Voluntary>>(@"Voluntary");

            ViewBag.volunteers = volunteers;
            
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
        
//        [HttpPost]
//        public ActionResult TrashAffinity()
//        {
//            return Json(new {status="success", message_title="Afinidade excluida com sucesso"});
//        }
//        
//        [HttpPost]
//        public ActionResult EditAffinity()
//        {
//            return Json(new {status="success", message_title="Afinidade editada com sucesso"});
//        }
//        
//        [HttpPost]
//        public ActionResult ApproveAffinity()
//        {
//            return Json(new {status="success", message_title="Afinidade aprovada com sucesso"});
//        }
        
        //######### AJAX VOLUNTARY ##########
        
        [HttpPost]
        public ActionResult TrashVoluntary(Guid id)
        {
         
            var response = clientHttp.Delete<List<Voluntary>>(@"Voluntary/", id);

            if ("200" == response.ToString())
            {
                return Json(new {status="success", message_title="Voluntário excluido com sucesso"});
            }

            return Json(new {status = "error", message_title = "Erro ao excluir voluntário"});
        }
        
        [HttpPost]
        public ActionResult ShowMoreVoluntary(Guid id)
        {
            var voluntary = clientHttp.Get<Voluntary>(string.Format(@"Voluntary/{0}", id.ToString()));
            return Json(new {status="success", voluntary=voluntary});
        }
        
        [HttpPost]
        public ActionResult ApproveVoluntary(Guid id)
        {
            Voluntary voluntary = (Voluntary)clientHttp.Get<Voluntary>(string.Format(@"Voluntary/{0}", id.ToString()));

            if (voluntary != null)
            {
                voluntary.IsApproved = true;
                
                var voluntary_id = clientHttp.Put<Voluntary>(@"Voluntary/", id, voluntary);
                
                return Json(new {status="success", message_title="Voluntário aprovado com sucesso"});
            }

            return Json(new {status = "error", message_title = "Erro ao aprovar voluntário"});
            
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
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
            if (!isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.user = "admin";
            ViewBag.index = "active";
            return View();
        }
        public ActionResult Register()
        {
            if (!isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.user = "admin";
            ViewBag.register = "active";
            
            var volunteers = clientHttp.Get<List<Voluntary>>(@"Voluntary");
            ViewBag.volunteers = volunteers;
            
            var entities= clientHttp.Get<List<Entity>>(@"Entity");
            ViewBag.entities = entities;
            
            var users = clientHttp.Get<List<User>>(@"User");
            ViewBag.users = users;
            
            return View();
        }
        
        
        //###################
        //### POST METHOD ###
        //###################
        
        [HttpPost]
        public ActionResult SaveModerator(User user)
        {
            if (!isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            if (ModelState.IsValid)
            {
                var id = clientHttp.Post<User>(@"User/", user);

                return RedirectToAction("Register");
            }
            
            ViewBag.user = "admin";
            ViewBag.register = "active";
            ViewBag.save_moderator_error = "true";
            
            var volunteers = clientHttp.Get<List<Voluntary>>(@"Voluntary");
            ViewBag.volunteers = volunteers;
            
            var users = clientHttp.Get<List<User>>(@"User");
            ViewBag.users = users;
            
            return View("Register",user);
        }
        
        
        //######### AJAX MODERATOR ##########
   
        [HttpPost]
        public ActionResult TrashModerator(Guid id)
        {
            
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
         
            var response = clientHttp.Delete<List<User>>(@"User/", id);

            if ("200" == response.ToString())
            {
                return Json(new {status="success", message_title="Usuário excluida com sucesso"});
            }

            return Json(new {status = "error", message_title = "Erro ao excluir a usuário"});
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
        


        //######### AJAX ENTITY ##########
   
        [HttpPost]
        public ActionResult TrashEntity(Guid id)
        {
            
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
         
            var response = clientHttp.Delete<List<Entity>>(@"Entity/", id);

            if ("200" == response.ToString())
            {
                return Json(new {status="success", message_title="Entidade excluida com sucesso"});
            }

            return Json(new {status = "error", message_title = "Erro ao excluir a entidade"});
        }

        public ActionResult ShowMoreEntity(Guid id)
        {
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
            var entity = clientHttp.Get<Entity>(string.Format(@"Entity/{0}", id.ToString()));
            return Json(new {status="success", entity=entity});
        }
        
        [HttpPost]
        public ActionResult ApproveEntity(Guid id)
        {
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
            Entity entity = (Entity)clientHttp.Get<Entity>(string.Format(@"Entity/{0}", id.ToString()));

            if (entity != null)
            {
                entity.IsApproved = true;
                var entity_id = clientHttp.Put<Entity>(@"Entity/", id, entity);
                
                return Json(new {status="success", message_title="Entidade aprovada com sucesso"});
            }

            return Json(new {status = "error", message_title = "Erro ao aprovar entidade"});
            
        }
        

        //######### AJAX VOLUNTARY ##########
        
        [HttpPost]
        public ActionResult TrashVoluntary(Guid id)
        {
         
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
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
            
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
            var voluntary = clientHttp.Get<Voluntary>(string.Format(@"Voluntary/{0}", id.ToString()));
            return Json(new {status="success", voluntary=voluntary});
        }
        
        [HttpPost]
        public ActionResult ApproveVoluntary(Guid id)
        {
            
            if (!isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
            Voluntary voluntary = (Voluntary)clientHttp.Get<Voluntary>(string.Format(@"Voluntary/{0}", id.ToString()));

            if (voluntary != null)
            {
                voluntary.IsApproved = true;
                
                var voluntary_id = clientHttp.Put<Voluntary>(@"Voluntary/", id, voluntary);
                
                return Json(new {status="success", message_title="Voluntário aprovado com sucesso"});
            }

            return Json(new {status = "error", message_title = "Erro ao aprovar voluntário"});
            
        }
        
        private bool isAuthenticated()
        {
            if (Session["user"]!=null)
            {
                User user = (User) Session["user"];
                if (user.IsApproved && user.IsModerator)
                    return true;
            }
            return false;
        }
        
    }
}
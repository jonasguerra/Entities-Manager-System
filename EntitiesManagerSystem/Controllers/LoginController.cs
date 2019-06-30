using System;
using System.Web.Mvc;
using EntitiesManagerSystem.Consumers_API;
using EntitiesManagerSystem.Models;

namespace EntitiesManagerSystem.Controllers
{
    public class LoginController : Controller
    {
        
        private APIHttpClient clientHttp;

        public LoginController()
        {
            clientHttp = new APIHttpClient("http://localhost:5002/api/");
        }
        
        // GET
        public ActionResult Login()
        {
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
                TempData.Remove("message");
            }
            return View();
        }
        
        
        [HttpPost]
        public ActionResult LoginMethod(LoginForm login)
        {
            if (ModelState.IsValid)
            {
                try { 
                    clientHttp.AuthenticationPost(login.Email, login.Password); 
                    
//                    TODO: retornar usuario e adicionar na session
//                    Session["user"]
                    
                    return RedirectToAction("Index", "Entity");
                }
                catch(Exception ex)
                {
                    return View("Login",login);
                }
            }
            
            return View("Login",login);
        }
        
        
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
        }
        
    }
}
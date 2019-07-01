using System;
using System.Web.Mvc;
using AdminManagerSystem.Consumers_API;
using AdminManagerSystem.Models;
using AdminManagerSystem.Models.Voluntary;

namespace AdminManagerSystem.Controllers
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
            if (Session["user"] != null)
            {
                User user = (User) Session["user"];
                if (user.IsModerator)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            
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
                    var u = clientHttp.AuthenticationPost(login.Email, login.Password); 
                    
                    var user = (User)clientHttp.Get<User>(string.Format(@"User/{0}", "f680cc5c-4c7d-4861-aa63-88b5f3c19348"));
                    Session["user"] = user;
                    if (user.IsModerator)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                catch(Exception ex)
                {
                    return RedirectToAction("Logout");
                }
            }
            return RedirectToAction("Logout");
        }
        
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login");
        }
    }
}
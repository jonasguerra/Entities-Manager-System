using System;
using System.Web.Mvc;
using EntitiesManagerSystem.Consumers_API;
using EntitiesManagerSystem.Models;
using EntitiesManagerSystem.Models.Voluntary;

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

            if (Session["user"] != null)
            {
                User user = (User) Session["user"];
                if (user.IsEntity)
                {
                    return RedirectToAction("Index", "Entity");
                }

                if (user.IsVoluntary)
                {
                    return RedirectToAction("Index", "Voluntary");
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
                    
//                    Session["user"] = clientHttp.Get<User>(string.Format(@"User/{0}", login.Email));
                    
//                    Gambiarra pra fazer funcionar, e seguir o projeto,
//                    não consegui fazer get do usuário de outra forma, e nem com a validação
                    var user = (User)clientHttp.Get<User>(string.Format(@"User/{0}", "ddc77478-09c2-4f10-9c16-2d581ad8a3fa"));

                    Session["user"] = user;
                    if (user.IsEntity)
                    {
                        return RedirectToAction("Index", "Entity");
                    }
                    if (user.IsVoluntary)
                    {
                        return RedirectToAction("Index", "Voluntary");
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
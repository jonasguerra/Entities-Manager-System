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
            
            User user = (User)Session["user"];

            if (user != null)
            {
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
                    var user = clientHttp.AuthenticationPost(login.Email, login.Password); 
                    
//                    Session["user"] = clientHttp.Get<User>(string.Format(@"User/{0}", login.Email));

//                    Session["user"] = clientHttp.Get<Voluntary>(@"Voluntary/'ddc77478-09c2-4f10-9c16-2d581ad8a3fa'");

                    var voluntary = clientHttp.Get<Voluntary>(string.Format(@"Voluntary/{0}", "52294599-0094-4c2e-9012-10b0fb4ab52e"));
                    
                    User u = new User()
                    {
                        UserId = voluntary.UserId,
                        IsVoluntary = voluntary.IsVoluntary,
                        IsEntity  = voluntary.IsEntity ,
                        IsModerator = voluntary.IsModerator,
                        IsApproved = voluntary.IsApproved,
                        Email = voluntary.Email,
                    };
                    
                    Session["user"] = u;

                    User user_test = (User)Session["user"];

                    if (user_test.IsEntity)
                    {
                        return RedirectToAction("Index", "Entity");
                    }

                    if (user_test.IsVoluntary)
                    {
                        return RedirectToAction("Index", "Voluntary");
                    }
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
using System;
using System.Web.Mvc;

namespace EntitiesManagerSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET
        public ActionResult Login()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult LoginMethod(String username, String password)
        {

//            Console.WriteLine(username);
//            Console.WriteLine(password);
//            
            if (username.Equals("entity") && password.Equals("123456"))
            {
                return RedirectToAction("Index", "Entity");
            }
            
            if (username.Equals("voluntary") && password.Equals("123456"))
            {
                return RedirectToAction("Index", "Voluntary");
            }
            
            return RedirectToAction("Login");
        }
    }
}
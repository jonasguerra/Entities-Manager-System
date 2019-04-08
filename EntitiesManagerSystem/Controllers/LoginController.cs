using System;
using System.Web.Mvc;

namespace EntitiesManagerSystem.Controllers
{
    public class LoginController : Controller
    {
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
        public ActionResult LoginMethod(String username, String password)
        {

            if (username.Equals("entity") && password.Equals("123456"))
            {
                return RedirectToAction("Index", "Entity");
            }
            
            if (username.Equals("voluntary") && password.Equals("123456"))
            {
                return RedirectToAction("Index", "Voluntary");
            }
            
            TempData["message"] = "Usuário ou senha não correspondem!";
            return RedirectToAction("Login");
        }
    }
}
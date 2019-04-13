using System;
using System.Web.Mvc;
using EntitiesManagerSystem.Models;

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
        public ActionResult LoginMethod(LoginForm login)
        {
            if (ModelState.IsValid)
            {
                if (login.Username.Equals("entity") && login.Password.Equals("123456"))
                {
                    return RedirectToAction("Index", "Entity");
                }
                if (login.Username.Equals("voluntary") && login.Password.Equals("123456"))
                {
                    return RedirectToAction("Index", "Voluntary");
                }
                TempData["message"] = "Usuário ou senha não correspondem!";
                return RedirectToAction("Login");
            }
            
            return View("Login",login);
        }
    }
}
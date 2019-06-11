using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntitiesManagerSystem.Consumers_API;
using EntitiesManagerSystem.Models.Voluntary;

namespace EntitiesManagerSystem.Controllers
{
    public class VoluntaryController : Controller
    {
        private APIHttpVoluntary voluntaryHttp;   
        public VoluntaryController()
        {
            voluntaryHttp = new APIHttpVoluntary("http://localhost:5002/api/");
        }

        
        // GET
        public ActionResult Index()
        {
            ViewBag.user = "voluntary";
            ViewBag.index = "active";
            
            return View();
        }
        public ActionResult Event()
        {
            ViewBag.user = "voluntary";
            ViewBag.events = "active";
            
            return View();
        }
        public ActionResult RegisterDonate()
        {
            ViewBag.user = "voluntary";
            ViewBag.register_donate = "active";
            
            return View();
        }
        
        public ActionResult RegisterVoluntary()
        {

            ViewBag.user = "voluntary";
            ViewBag.index = "active";
            
            return View("RegisterVoluntary");
                
        }
        
        //###################
        //### POST METHOD ###
        //###################
        
        [HttpPost]
        public ActionResult SaveDonation(Donations donation)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            ViewBag.user = "voluntary";
            ViewBag.register_donate = "active";
            return View("RegisterDonate",donation);
        }
        
        [HttpPost]
        public ActionResult SaveVoluntary(Voluntary voluntary)
        {
            if (ModelState.IsValid)
            {
                var id = voluntaryHttp.Post<Voluntary>(@"Voluntary/", voluntary);
                
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.user = "voluntary";
            ViewBag.index = "active";
            return View("RegisterVoluntary", voluntary);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntitiesManagerSystem.Models.Voluntary;

namespace EntitiesManagerSystem.Controllers
{
    public class VoluntaryController : Controller
    {
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
            ViewBag.category_event = new  List<String>
            {
                "Pets", 
                "Infantil", 
                "Idosos",
                "Necessitados",
                "Meio Ambiente",
            };
            
            return View();
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
            
            return View("RegisterDonate",donation);
        }
    }
}
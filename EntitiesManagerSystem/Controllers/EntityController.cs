using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntitiesManagerSystem.Models;

namespace EntitiesManagerSystem.Controllers
{
    public class EntityController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.user = "entity";
            ViewBag.index = "active";
            
            return View();
        }
        
        public ActionResult RegisterEvent()
        {

            ViewBag.user = "entity";
            ViewBag.register_event = "active";
            
            return View();
        }
        public ActionResult Donations()
        {

            ViewBag.user = "entity";
            ViewBag.donations = "active";
            
            return View();
        }
        
        public ActionResult RegisterEntity()
        {
            ViewBag.user = "entity";
            ViewBag.category_event = new List<String>
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
        public ActionResult SaveEvent(Event event_from)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            
            
            
            ViewBag.user = "entity";
            ViewBag.register_event = "active";
            return View("RegisterEvent",event_from);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntitiesManagerSystem.Consumers_API;
using EntitiesManagerSystem.Models;
using Newtonsoft.Json;

namespace EntitiesManagerSystem.Controllers
{
    public class EntityController : Controller
    {
        
        private APIHttpClient clientHttp;   
        public EntityController()
        {
            clientHttp = new APIHttpClient("http://localhost:5002/api/");
        }
        
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
            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
            ViewBag.affinities = affinities;
            
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
                dynamic json_affinity = JsonConvert.DeserializeObject(event_from.Affinity);
                event_from.Affinities = new List<Affinity>();
                foreach (var affinity in json_affinity)
                {
                    event_from.Affinities.Add(new Affinity()
                    {
                        AffinityId = Guid.Parse(affinity["value"].ToString()),
                        Name = affinity["text"]
                    });
                }

                var id = clientHttp.Post<Event>(@"Event/", event_from);
                
                return RedirectToAction("Index");
            }
            
            ViewBag.user = "entity";
            ViewBag.register_event = "active";
            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
            ViewBag.affinities = affinities;
            return View("RegisterEvent",event_from);
        }
    }
}
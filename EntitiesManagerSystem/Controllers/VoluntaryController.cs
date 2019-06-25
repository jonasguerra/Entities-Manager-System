using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EntitiesManagerSystem.Consumers_API;
using EntitiesManagerSystem.Models;
using EntitiesManagerSystem.Models.Voluntary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EntitiesManagerSystem.Controllers
{
    public class VoluntaryController : Controller
    {
        private APIHttpClient clientHttp;   
        public VoluntaryController()
        {
            clientHttp = new APIHttpClient("http://localhost:5002/api/");
        }

        
        // GET
        public ActionResult Index()
        {
            ViewBag.user = "voluntary";
            ViewBag.index = "active";
            
            return View();
        }
        public ActionResult Events()
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
            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
            
            ViewBag.affinities = affinities;
            
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
            
            ViewBag.user = "voluntary";
            ViewBag.register_donate = "active";
            return View("RegisterDonate",donation);
        }
        
        [HttpPost]
        public ActionResult SaveVoluntary(Voluntary voluntary)
        {
            if (ModelState.IsValid)
            {
                dynamic json_affinity = JsonConvert.DeserializeObject(voluntary.Affinity);
                voluntary.Affinities = new List<Affinity>();
                foreach (var affinity in json_affinity)
                {
                    voluntary.Affinities.Add(new Affinity()
                    {
                        AffinityId = Guid.Parse(affinity["value"].ToString()),
                        Name = affinity["text"]
                    });
                }

                var id = clientHttp.Post<Voluntary>(@"Voluntary/", voluntary);
                
                return RedirectToAction("Login", "Login");
            }
            
            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
            ViewBag.affinities = affinities;
            return View("RegisterVoluntary", voluntary);
        }
        
        
        [HttpPost]
        public ActionResult ShowMoreEvent()
        {
            Event show_event = new Event()
            {
                EventId = new Guid(),
                Title = "Evento",
                Description = "Nam sed quam vitae augue viverra convallis. Aliquam at ultricies tortor, eu tempor arcu. Pellentesque tincidunt ante nec mattis sagittis. Donec sollicitudin tortor vel felis consectetur dictum.",
            };

            return Json(new {status="success", show_event=show_event});
        }
    }
}
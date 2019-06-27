using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Helpers;
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
            
            Guid voluntaryId = Guid.Parse("4d293167-6358-4d91-b480-2d4ac80b162b");
            Guid eventId = Guid.Parse("f650b4ac-6cbe-4c3b-9ccb-736c70153694");
                
            EventVoluntary event_voluntary = new EventVoluntary()
            {
                EventId = eventId,
                VoluntaryId = voluntaryId
            };
            
            var event_id = clientHttp.Post<EventVoluntary>(@"Event/SetVoluntaryToEvent/", event_voluntary);
            
            return RedirectToAction("RegisterVoluntary");
            
//            if (ModelState.IsValid)
//            {
//                dynamic json_affinity = JsonConvert.DeserializeObject(voluntary.Affinity);
//                voluntary.Affinities = new List<Affinity>();
//                foreach (var affinity in json_affinity)
//                {
//                    voluntary.Affinities.Add(new Affinity()
//                    {
//                        AffinityId = Guid.Parse(affinity["value"].ToString()),
//                        Name = affinity["text"]
//                    });
//                }
//
//                var id = clientHttp.Post<Voluntary>(@"Voluntary/", voluntary);
//                
//                return RedirectToAction("Login", "Login");
//            }
//            
//            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
//            ViewBag.affinities = affinities;
//            return View("RegisterVoluntary", voluntary);
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


        [HttpPost]
        public ActionResult SetVoluntaryToEvent(Guid voluntarayId, Guid eventId)
        {
            
            dynamic data = new ExpandoObject();
            data.voluntarayId = voluntarayId;
            data.eventId = eventId;

            var id = clientHttp.Post<Voluntary>(@"Voluntary/SetVoluntaryToEvent/", data);
            return Json(new {status="success"});
        }
    }
}
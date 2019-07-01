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
            if (isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.user = "voluntary";
            ViewBag.index = "active";

            return View();
        }

        public ActionResult Events()
        {
            if (isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.user = "voluntary";
            ViewBag.events = "active";
            
            var events = clientHttp.Get<List<Event>>(@"Event");

            ViewBag.allEvents = events;

            return View();
        }

        public ActionResult RegisterDonate()
        {
            if (isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            ViewBag.user = "voluntary";
            ViewBag.register_donate = "active";

            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
            ViewBag.affinities = affinities;
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
        public ActionResult SaveDonation(Donation donation)
        {
            if (isAuthenticated())
            {
                return RedirectToAction("Login", "Login");
            }
            
            if (ModelState.IsValid)
            {
                dynamic json_affinity = JsonConvert.DeserializeObject(donation.Affinity);
                donation.Affinities = new List<Affinity>();
                foreach (var affinity in json_affinity)
                {
                    donation.Affinities.Add(new Affinity()
                    {
                        AffinityId = Guid.Parse((affinity["value"].ToString())),
                        Name = affinity["text"]
                    });
                }

                var id = clientHttp.Post<Donation>(@"Donation/", donation);

                return RedirectToAction("RegisterDonate","Voluntary");
            }

            ViewBag.user = "voluntary";
            ViewBag.register_donate = "active";
            var affinities = clientHttp.Get<List<Affinity>>(@"Affinity");
            ViewBag.affinities = affinities;
            
            return View("RegisterDonate", donation);
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

        
        //######### AJAX EVENT ##########

        [HttpPost]
        public ActionResult ShowMoreEvent(Guid id)
        {
            if (isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
            var sEvent = clientHttp.Get<Event>(string.Format(@"Event/{0}", id.ToString()));
            return Json(new {status="success", sEvent=sEvent});
        }


        [HttpPost]
        public ActionResult SetVoluntaryToEvent(Guid voluntaryId, Guid eventId)
        {
            if (isAuthenticated())
            {
                return Json(new {status = "error"});
            }
            
            EventVoluntary event_voluntary = new EventVoluntary()
            {
                EventId = eventId,
                VoluntaryId = voluntaryId
            };

            var id = clientHttp.Post<EventVoluntary>(@"Voluntary/SetVoluntaryToEvent/", event_voluntary);
            return Json(new {status = "success"});
        }

        private bool isAuthenticated()
        {
            User user = (User) Session["user"];
            
            if (!user.IsApproved || !user.IsVoluntary)
                return false;
            
            return true;
        }
    }
}
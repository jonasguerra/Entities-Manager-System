using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminManagerSystem.Models.Voluntary;
using Ftec.WebAPI.Infra.Repository;
using Manager_API.Models;
using Manager_Application;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;

namespace Manager_API.Controllers 
{
    public class EventController : ApiController
    {
        private IEventRepository eventRepository;
        private EventApplication eventApplication;

        public EventController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            eventRepository = new EventRepository(connectionString);
            eventApplication = new EventApplication(eventRepository);
        }
        
        public HttpResponseMessage Get()
        {
            List<EventDTO> eventDTO = new List<EventDTO>();
            try
            {
                eventDTO  = eventApplication.GetAll();
       
                //Este metodo retorna uma listagem de voluntarios
                return Request.CreateResponse(HttpStatusCode.OK, eventDTO );
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                EventDTO eventDTO = Find(id);
                if (eventDTO == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Evento não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, eventDTO);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("api/Event/SetVoluntaryToEvent/")]
        [HttpPost]
        public HttpResponseMessage SetVoluntaryToEvent([FromBody]EventVoluntary eventVoluntary)
        {
            try
            {
                //######## THIS IS THE MASTER GAMBIARRA ########
                //http://www.macoratti.net/17/10/c_odynamic1.htm
                
                Console.WriteLine(eventVoluntary.EventId);
                Console.WriteLine(eventVoluntary.VoluntaryId);
                
                bool save = eventApplication.SetVoluntaryToEvent(eventVoluntary.VoluntaryId, eventVoluntary.EventId);
                if (save)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, eventVoluntary.EventId);
                }
                return Request.CreateResponse(HttpStatusCode.OK, eventVoluntary.EventId);
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Post([FromBody]Event sEvent)
        {
            try
            {
                Guid id = Insert(sEvent);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Put(Guid id, [FromBody]Event sEvent)
        {
            try
            {
                EventDTO eventDto = Find(id);
                if (eventDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Evento não encontrado");
                }
                else
                {
                    Alter(sEvent);
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                EventDTO eventDTO = Find(id);
                if (eventDTO == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Evento não encontrado");
                }
                else
                {
                    bool removed = Remove(id);
                    if (removed)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, id);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro ao excluir evento");
        }
        
        private void Alter(Event sEvent)
        {
            
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            foreach(var affinity in sEvent.Affinities)
            {
                AffinityDTO affinityDTO = new AffinityDTO()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                affinities.Add(affinityDTO);
            }
            
            EventDTO eventDTO = new EventDTO ()
            {
                EventId = sEvent.EventId,
                Title = sEvent.Title,
                Description = sEvent.Description,
                Date = sEvent.Date,
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    CEP = sEvent.Address.CEP,
                    Avenue = sEvent.Address.Avenue,
                    Number = sEvent.Address.Number,
                    Neighborhood = sEvent.Address.Neighborhood,
                    City = sEvent.Address.City,
                    State = sEvent.Address.State
                }
            };
            
            eventApplication.Update(eventDTO);
        }
        
        private Guid Insert(Event sEvent)
        {
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            foreach(var affinity in sEvent.Affinities)
            {
                AffinityDTO affinityDTO = new AffinityDTO()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                affinities.Add(affinityDTO);
            }
            
            EventDTO eventDto = new EventDTO ()
            {
                EventId = sEvent.EventId,
                Title = sEvent.Title,
                Description = sEvent.Description,
                Date = sEvent.Date,
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    CEP = sEvent.Address.CEP,
                    Avenue = sEvent.Address.Avenue,
                    Number = sEvent.Address.Number,
                    Neighborhood = sEvent.Address.Neighborhood,
                    City = sEvent.Address.City,
                    State = sEvent.Address.State
                }
            };
           
            return eventApplication.Insert(eventDto);
        }
        
        private EventDTO Find(Guid id)
        {
            return eventApplication.Get(id);
        }
        
        private bool Remove(Guid id)
        {
            return eventApplication.Delete(id);
        }
    }
}
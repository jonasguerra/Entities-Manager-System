using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ftec.WebAPI.Infra.Repository;
using Manager_Application;
using Manager_Application.DTO;
using Manager_Domain.Entities;
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
        
        public HttpResponseMessage Post([FromBody]Event sevent)
        {
            try
            {
                Guid id = Insert(sevent);
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
        
        public HttpResponseMessage Put(Guid id, [FromBody]Event sevent)
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
                    Alter(sevent);
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
        
        private void Alter(Event sevent)
        {
            
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            foreach(var affinity in sevent.Affinities)
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
                EventId = sevent.EventId,
                Title = sevent.Title,
                Description = sevent.Description,
                Date = sevent.Date,
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    CEP = sevent.Address.CEP,
                    Avenue = sevent.Address.Avenue,
                    Number = sevent.Address.Number,
                    Neighborhood = sevent.Address.Neighborhood,
                    City = sevent.Address.City,
                    State = sevent.Address.State
                }
            };
            
            eventApplication.Update(eventDTO);
        }
        
        private Guid Insert(Event sevent)
        {
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            foreach(var affinity in sevent.Affinities)
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
                EventId = sevent.EventId,
                Title = sevent.Title,
                Description = sevent.Description,
                Date = sevent.Date,
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    CEP = sevent.Address.CEP,
                    Avenue = sevent.Address.Avenue,
                    Number = sevent.Address.Number,
                    Neighborhood = sevent.Address.Neighborhood,
                    City = sevent.Address.City,
                    State = sevent.Address.State
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
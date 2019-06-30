using System;
using System.Collections.Generic;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

namespace Manager_Application
{
    public class EventApplication
    {
        IEventRepository eventRepository;

        public EventApplication(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }
        
        public Guid Insert(EventDTO eventDto)
        {
            eventDto.EventId = Guid.NewGuid();
            eventDto.Address.AddressId = Guid.NewGuid();
            var sEvent = EventAdapter.ToDomain(eventDto);
            return eventRepository.Insert(sEvent);
        }

        public Guid Update(EventDTO eventDto)
        {
            var sEvent = EventAdapter.ToDomain(eventDto);
            return eventRepository.Update(sEvent);
        }

        public bool Delete(Guid id)
        {
            return eventRepository.Delete(id);
        }

        public bool SetVoluntaryToEvent(Guid voluntarayId, Guid eventId)
        {
            return eventRepository.SetVoluntaryToEvent(voluntarayId, eventId);
        }
        
        public EventDTO Get(Guid id)
        {
            var sEvent = eventRepository.Find(id);
            return EventAdapter.ToDTO(sEvent);
        }

        public List<EventDTO> GetAll()
        {
            List<EventDTO> eventsDto = new List<EventDTO>();
            var events = eventRepository.FindAll();
            foreach (Event cli in events)
            {
                eventsDto.Add(EventAdapter.ToDTO(cli));
            }
            return eventsDto;
        }
        
    }
}
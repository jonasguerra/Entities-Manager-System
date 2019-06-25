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
            var sevent = EventAdapter.ToDomain(eventDto);

            return eventRepository.Insert(sevent);
        }

        public Guid Update(EventDTO eventDto)
        {
            var sevent = EventAdapter.ToDomain(eventDto);

            return eventRepository.Update(sevent);
        }

        public bool Delete(Guid id)
        {
            return eventRepository.Delete(id);
        }

        public EventDTO Get(Guid id)
        {
            var sevent = eventRepository.Find(id);

            return EventAdapter.ToDTO(sevent);
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
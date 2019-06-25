using System;
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public class EventAdapter
    {
        public static EventDTO ToDTO(Event sevent)
        {
            return new EventDTO()
            {
                EventId = sevent.EventId,
                Title = sevent.Title,
                Description = sevent.Description,
                Date = sevent.Date,
                Affinities = AffinityAdapter.ListToDTO(sevent.Affinities),
                Address = new AddressDTO()
                {
                    AddressId = sevent.Address.AddressId,
                    CEP = sevent.Address.CEP,
                    Avenue = sevent.Address.Avenue,
                    Number = sevent.Address.Number,
                    Neighborhood = sevent.Address.Neighborhood,
                    City = sevent.Address.City,
                    State = sevent.Address.State
                }
            };
        }

        public static Event ToDomain(EventDTO sevent)
        {
            return new Event()
            {
                EventId = sevent.EventId,
                Title = sevent.Title,
                Description = sevent.Description,
                Date = sevent.Date,
                Affinities = AffinityAdapter.ListToDomain(sevent.Affinities),
                Address = new Address()
                {
                    AddressId = sevent.Address.AddressId,
                    CEP = sevent.Address.CEP,
                    Avenue = sevent.Address.Avenue,
                    Number = sevent.Address.Number,
                    Neighborhood = sevent.Address.Neighborhood,
                    City = sevent.Address.City,
                    State = sevent.Address.State
                }
            };
        }
    }
}
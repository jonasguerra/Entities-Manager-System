using System;
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public class EventAdapter
    {
        public static EventDTO ToDTO(Event sEvent)
        {
            return new EventDTO()
            {
                EventId = sEvent.EventId,
                Title = sEvent.Title,
                Description = sEvent.Description,
                Date = sEvent.Date,
                Affinities = AffinityAdapter.ListToDTO(sEvent.Affinities),
                Address = new AddressDTO()
                {
                    AddressId = sEvent.Address.AddressId,
                    CEP = sEvent.Address.CEP,
                    Avenue = sEvent.Address.Avenue,
                    Number = sEvent.Address.Number,
                    Neighborhood = sEvent.Address.Neighborhood,
                    City = sEvent.Address.City,
                    State = sEvent.Address.State
                }
            };
        }

        public static Event ToDomain(EventDTO sEvent)
        {
            return new Event()
            {
                EventId = sEvent.EventId,
                Title = sEvent.Title,
                Description = sEvent.Description,
                Date = sEvent.Date,
                Affinities = AffinityAdapter.ListToDomain(sEvent.Affinities),
                Address = new Address()
                {
                    AddressId = sEvent.Address.AddressId,
                    CEP = sEvent.Address.CEP,
                    Avenue = sEvent.Address.Avenue,
                    Number = sEvent.Address.Number,
                    Neighborhood = sEvent.Address.Neighborhood,
                    City = sEvent.Address.City,
                    State = sEvent.Address.State
                }
            };
        }
    }
}
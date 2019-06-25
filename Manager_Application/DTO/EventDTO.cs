using System;
using System.Collections.Generic;

namespace Manager_Application.DTO
{
    public class EventDTO
    {
        public Guid EventId { get; set; }
        public AddressDTO Address { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<AffinityDTO> Affinities { get; set; }
    }
}
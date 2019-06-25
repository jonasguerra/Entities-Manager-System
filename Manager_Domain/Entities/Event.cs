using System;
using System.Collections.Generic;

namespace Manager_Domain.Entities
{
    public class Event
    {
        public Guid EventId { get; set; }
        public Address Address { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<Affinity> Affinities { get; set; }

        public Event()
        {
        }
    }
}
using System;
using System.Collections.Generic;

namespace Manager_Domain.Entities
{
    public class Voluntary : User
    {
        public Guid VoluntaryId { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string SocialNetwork { get; set; }
        public string PhotoImageName { get; set; }
        public List<Affinity> Affinities { get; set; }
    }
}
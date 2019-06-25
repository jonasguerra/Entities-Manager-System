using System;
using System.Collections.Generic;

namespace Manager_Domain.Entities
{
    public class Donation : User
    {

        public Guid DonationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public bool takeDonation { get; set; }
        public Address Address { get; set; }
        public List<Affinity> Affinities { get; set; }
        
        public Donation()
        {
            
        }
    }
}
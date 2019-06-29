using System;
using System.Collections.Generic;

namespace Manager_Application.DTO
{
    public class DonationDTO : UserDTO
    {
        public Guid DonationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public bool TakeDonation { get; set; }
        public AddressDTO Address { get; set; }
        public List<AffinityDTO> Affinities { get; set; }

        public DonationDTO()
        {
        }
    }
}
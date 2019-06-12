using System;

namespace Manager_Application.DTO
{
    public class VoluntaryDTO : UserDTO
    {
        public Guid VoluntaryId { get; set; }
        public AddressDTO Address { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Affinity { get; set; }
        public string SocialNetwork { get; set; }
        public string PhotoImageName { get; set; }
    }
}
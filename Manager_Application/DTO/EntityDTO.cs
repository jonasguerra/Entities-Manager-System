using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Application.DTO
{
    public class EntityDTO : UserDTO
    {
        public Guid Id { get; set; }

        public string EntityName { get; set; }
        public string EntityResponsableName { get; set; }
        
        public string EntityPhone { get; set; }
        
        public AddressDTO EntityAddress { get; set; }
        public string EntityReferencePoint { get; set; }
        //public string EntityAffinity { get; set; }
        public List<AffinityDTO> EntityAffinity { get; set; }
        public string EntityInitials { get; set; }
        public DateTime EntityCreationDate { get; set; }
        public string EntitySocialNetwork { get; set; }
        public string EntityWebSite { get; set; }
        public string EntityDescription { get; set; }
       
        public EntityDTO()
        {
            // IsEntity= true; 
        }
    }
}
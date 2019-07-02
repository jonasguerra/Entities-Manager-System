using System;
using System.Collections.Generic;

namespace Manager_Domain.Entities
{
    public class Entity : User
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityResponsableName { get; set; }

        public string EntityPhone { get; set; }
     
        public Address EntityAddress { get; set; }
        public string EntityReferencePoint { get; set; }
        public List<Affinity> EntityAffinity { get; set; }
        public string EntityInitials { get; set; }
        public DateTime EntityCreationDate { get; set; }
        public string EntitySocialNetwork { get; set; }
        public string EntityWebSite { get; set; }
        public string EntityDescription { get; set; }
     

        public Entity()
        {
            IsEntity = true;
        }
    }
}
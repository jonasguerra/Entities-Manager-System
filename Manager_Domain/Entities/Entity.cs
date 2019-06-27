using System;

namespace Manager_Domain.Entities
{
    public class Entity : User
    {
        public Guid EntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityResponsableName { get; set; }
        public string EntityEmail { get; set; }
        public string EntityPhone { get; set; }
        public string EntityPassword { get; set; }
        public string EntityConfirmPassword { get; set; }
        public Address EntityAddress { get; set; }
        public string EntityReferencePoint { get; set; }
        public string EntityAffinity { get; set; }
        public string EntityInitials { get; set; }
        public DateTime EntityCreationDate { get; set; }
        public string EntitySocialNetwork { get; set; }
        public string EntityWebSite { get; set; }
        public string EntityDescription { get; set; }
        public string EntityPhotoImageName { get; set; }

        public Entity()
        {
            //IsEntity = true;
        }
    }
}
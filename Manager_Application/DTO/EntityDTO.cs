using System;

namespace Manager_Application.DTO
{
    public class EntityDTO
    {
        public Guid Id { get; set; }

        public string EntityName { get; set; }
        public string EntityResponsableName { get; set; }
        public string EntityEmail { get; set; }
        public string EntityPhone { get; set; }
        public string EntityPassword { get; set; }
        public string EntityConfirmPassword { get; set; }
        public string EntityCEP { get; set; }
        public string EntityAvenue { get; set; }
        public string EntityNumber { get; set; }
        public string EntityNeighborhood { get; set; }
        public string EntityCity { get; set; }
        public string EntityState { get; set; }
        public string EntityReferencePoint { get; set; }
        public string EntityAffinity { get; set; }
        public string EntityInitials { get; set; }
        public DateTime EntityCreationDate { get; set; }
        public string EntitySocialNetwork { get; set; }
        public string EntityWebSite { get; set; }
        public string EntityDescription { get; set; }
        public string EntityPhotoImageName { get; set; }

    }
}
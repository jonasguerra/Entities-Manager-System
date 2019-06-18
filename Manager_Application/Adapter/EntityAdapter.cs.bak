using System;
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public static class EntityAdapter
    {
        public static EntityDTO ToDTO(Entity entity)
        {
            return new EntityDTO()
            {
                 EntityName = entity.EntityName,
                 EntityResponsableName = entity.EntityResponsableName,
                 EntityEmail = entity.EntityEmail,
                 EntityPhone = entity.EntityPhone,
                 EntityPassword = entity.EntityPassword,
                 EntityConfirmPassword = entity.EntityConfirmPassword,
                 EntityCEP = entity.EntityCEP, 
                 EntityAvenue = entity.EntityAvenue,
                 EntityNumber = entity.EntityNumber,
                 EntityNeighborhood = entity.EntityNeighborhood,
                 EntityCity = entity.EntityCity,
                 EntityState = entity.EntityState,
                 EntityReferencePoint = entity.EntityReferencePoint,
                 EntityAffinity = entity.EntityAffinity,
                 EntityInitials = entity.EntityInitials,
                 EntityCreationDate = entity.EntityCreationDate ,
                 EntitySocialNetwork = entity.EntitySocialNetwork,
                 EntityWebSite = entity.EntityWebSite,
                 EntityDescription = entity.EntityDescription,
                 EntityPhotoImageName = entity.EntityPhotoImageName,
             
            };
        }

        public static Entity ToDomain(EntityDTO entity)
        {
            Console.WriteLine("POST METHOD 4");
            
            return new Entity()
            {
                EntityName = entity.EntityName,
                EntityResponsableName = entity.EntityResponsableName,
                EntityEmail = entity.EntityEmail,
                EntityPhone = entity.EntityPhone,
                EntityPassword = entity.EntityPassword,
                EntityConfirmPassword = entity.EntityConfirmPassword,
                EntityCEP = entity.EntityCEP, 
                EntityAvenue = entity.EntityAvenue,
                EntityNumber = entity.EntityNumber,
                EntityNeighborhood = entity.EntityNeighborhood,
                EntityCity = entity.EntityCity,
                EntityState = entity.EntityState,
                EntityReferencePoint = entity.EntityReferencePoint,
                EntityAffinity = entity.EntityAffinity,
                EntityInitials = entity.EntityInitials,
                EntityCreationDate = entity.EntityCreationDate ,
                EntitySocialNetwork = entity.EntitySocialNetwork,
                EntityWebSite = entity.EntityWebSite,
                EntityDescription = entity.EntityDescription,
                EntityPhotoImageName = entity.EntityPhotoImageName,
            };
        }
    }
}
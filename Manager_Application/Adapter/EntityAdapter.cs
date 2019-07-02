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
                 Id = entity.EntityId,
                 UserId = entity.UserId,
                 IsApproved = entity.IsApproved,
                 EntityName = entity.EntityName,
                 EntityResponsableName = entity.EntityResponsableName,
                 Email = entity.Email,
                 EntityPhone = entity.EntityPhone,
                 Password = entity.Password,
                 EntityReferencePoint = entity.EntityReferencePoint,
                 EntityAffinity = AffinityAdapter.ListToDTO(entity.EntityAffinity),
                 EntityInitials = entity.EntityInitials,
                 EntityCreationDate = entity.EntityCreationDate ,
                 EntitySocialNetwork = entity.EntitySocialNetwork,
                 EntityWebSite = entity.EntityWebSite,
                 EntityDescription = entity.EntityDescription,
               
                 
                 EntityAddress = new AddressDTO()
                 {
                     AddressId = entity.EntityAddress.AddressId,
                     CEP = entity.EntityAddress.CEP,
                     Avenue = entity.EntityAddress.Avenue,
                     Number = entity.EntityAddress.Number,
                     Neighborhood = entity.EntityAddress.Neighborhood,
                     City = entity.EntityAddress.City,
                     State = entity.EntityAddress.State
                 }  
            };
        }

        public static Entity ToDomain(EntityDTO entity)
        {
            return new Entity()
            {
                UserId = entity.UserId,
                EntityId =  entity.Id,
                IsApproved = entity.IsApproved,
                EntityName = entity.EntityName,
                EntityResponsableName = entity.EntityResponsableName,
                Email = entity.Email,
                EntityPhone = entity.EntityPhone,
                Password = entity.Password,
                EntityReferencePoint = entity.EntityReferencePoint,
                EntityAffinity = AffinityAdapter.ListToDomain(entity.EntityAffinity),
                EntityInitials = entity.EntityInitials,
                EntityCreationDate = entity.EntityCreationDate ,
                EntitySocialNetwork = entity.EntitySocialNetwork,
                EntityWebSite = entity.EntityWebSite,
                EntityDescription = entity.EntityDescription,
            
                EntityAddress = new Address()
                {
                    AddressId = entity.EntityAddress.AddressId,
                    CEP = entity.EntityAddress.CEP,
                    Avenue = entity.EntityAddress.Avenue,
                    Number = entity.EntityAddress.Number,
                    Neighborhood = entity.EntityAddress.Neighborhood,
                    City = entity.EntityAddress.City,
                    State = entity.EntityAddress.State
                }
            };
        }
    }
}
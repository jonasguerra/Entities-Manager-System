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
                UserId = entity.UserId,
                Id = entity.EntityId,
                EntityName = entity.EntityName,
                 EntityResponsableName = entity.EntityResponsableName,
                 Email = entity.Email,
                 EntityPhone = entity.EntityPhone,
                 Password = entity.Password,
                 EntityReferencePoint = entity.EntityReferencePoint,
                // EntityAffinity = entity.EntityAffinity,
                 EntityAffinity = AffinityAdapter.ListToDTO(entity.EntityAffinity),
                 EntityInitials = entity.EntityInitials,
                 EntityCreationDate = entity.EntityCreationDate ,
                 EntitySocialNetwork = entity.EntitySocialNetwork,
                 EntityWebSite = entity.EntityWebSite,
                 EntityDescription = entity.EntityDescription,
               
                 
                 EntityAddressDto = new AddressDTO()
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
                    AddressId = entity.EntityAddressDto.AddressId,
                    CEP = entity.EntityAddressDto.CEP,
                    Avenue = entity.EntityAddressDto.Avenue,
                    Number = entity.EntityAddressDto.Number,
                    Neighborhood = entity.EntityAddressDto.Neighborhood,
                    City = entity.EntityAddressDto.City,
                    State = entity.EntityAddressDto.State
                }
            };
        }
    }
}
using System;
using System.Collections.Generic;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

namespace Manager_Application
{
    public class EntityApplication
    {
        IEntityRepository entityRepository;

        public EntityApplication(IEntityRepository entityRepository)
        {
            this.entityRepository = entityRepository;
        }

        public Guid Insert(EntityDTO entityDto)
        {


            entityDto.UserId = Guid.NewGuid();
            entityDto.Id= Guid.NewGuid();
            entityDto.EntityAddressDto.AddressId= Guid.NewGuid();
            var entity = EntityAdapter.ToDomain(entityDto);

            return entityRepository.Insert(entity);
        }

        public Guid Update(EntityDTO entityDto)
        {
            var entity = EntityAdapter.ToDomain(entityDto);

            return entityRepository.Update(entity);
        }

        public bool Delete(Guid id)
        {
            return entityRepository.Delete(id);
        }

        public EntityDTO Get(Guid id)
        {
            var entity = entityRepository.Find(id);

            return EntityAdapter.ToDTO(entity);
        }

        public List<EntityDTO> GetAll()
        {
            List<EntityDTO> entitysDto = new List<EntityDTO>();
            var entitys = entityRepository.FindAll();


            foreach(Entity cli in entitys)
            {
                entitysDto.Add(EntityAdapter.ToDTO(cli));
            }

            return entitysDto;
        }
    }
}
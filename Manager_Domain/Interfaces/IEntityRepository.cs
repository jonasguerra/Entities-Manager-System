using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IEntityRepository
    {
        Guid Insert(Entity entity);
        Entity Find(Guid id);
        List<Entity> FindAll();
        Guid Update(Entity entity);
        bool Delete(Guid id);
    }
}
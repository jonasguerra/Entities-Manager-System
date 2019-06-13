using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IAffinityRepository
    {
        Guid Insert(Affinity affinity);
        Affinity Find(Guid id);
        List<Affinity> FindAll();
        Guid Update(Affinity voluntary);
        bool Delete(Guid id);
    }
}
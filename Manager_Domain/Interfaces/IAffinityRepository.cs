using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IAffinityRepository
    {
//        Guid Insert(Affinity affinity);
        Affinity Find(Guid id);
        List<Affinity> FindAll();
//        List<Affinity> FindByVoluntaryId(Guid id);
//        List<Affinity> FindByEntityId(Guid id);
//        List<Affinity> FindByEventId(Guid id);
//        List<Affinity> FindByDonationId(Guid id);
//        Guid Update(Affinity voluntary);
//        bool Delete(Guid id);
    }
}
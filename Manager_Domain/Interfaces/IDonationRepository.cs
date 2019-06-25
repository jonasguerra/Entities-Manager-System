using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IDonationRepository
    {
        Guid Insert(Donation donation);
        Donation Find(Guid id);
        List<Donation> FindByAffinityId(Guid id);
        List<Donation> FindAll();
        Guid Update(Donation donation);
        bool Delete(Guid id);
    }
}
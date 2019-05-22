using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IVoluntaryRepository
    {
        Guid Insert(Voluntary cliente);
        Voluntary Find(Guid id);
        List<Voluntary> FindAll();
        Guid Update(Voluntary cliente);
        bool Delete(Guid id);
    }
}
using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IVoluntaryRepository
    {
        Guid Insert(Voluntary voluntary);
        Voluntary Find(Guid id);
        List<Voluntary> FindAll();
        Guid Update(Voluntary voluntary);
        bool Delete(Guid id);
    }
}
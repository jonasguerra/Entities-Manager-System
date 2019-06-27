using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IEventRepository
    {
        Guid Insert(Event sEvent);
        bool SetVoluntaryToEvent(Guid voluntarayId, Guid eventId);
        Event Find(Guid id);
        List<Event> FindByAffinityId(Guid id);
        List<Event> FindAll();
        Guid Update(Event sEvent);
        bool Delete(Guid id);
    }
}
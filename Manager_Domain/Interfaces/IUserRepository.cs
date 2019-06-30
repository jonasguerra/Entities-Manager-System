using System;
using System.Collections.Generic;
using Manager_Domain.Entities;

namespace Manager_Domain.Interfaces
{
    public interface IUserRepository
    {
        Guid Insert(User user);
        User Find(Guid id);
        User Find(string email);
        List<User> FindAll();
        Guid Update(User user);
        bool Delete(Guid id);
    }
}
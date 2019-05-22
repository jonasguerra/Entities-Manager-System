using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

namespace Ftec.WebAPI.Infra.Repository
{
    public class VoluntaryRepository : IVoluntaryRepository
    {
        private string connectionString;

        public VoluntaryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Voluntary Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Voluntary> FindAll()
        {
            throw new NotImplementedException();
        }

        public Guid Insert(Voluntary cliente)
        {
            Console.WriteLine("POST METHOD 5");
            
            throw new NotImplementedException();
        }

        public Guid Update(Voluntary cliente)
        {
            throw new NotImplementedException();
        }
    }
}
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
            Console.WriteLine("DELETE REPOSITORY");
            throw new NotImplementedException();
        }

        public Voluntary Find(Guid id)
        {
            Console.WriteLine("GET ONE REPOSITORY");
            throw new NotImplementedException();
        }

        public List<Voluntary> FindAll()
        {
            Console.WriteLine("GET ALL REPOSITORY");
            throw new NotImplementedException();
        }

        public Guid Insert(Voluntary cliente)
        {
            Console.WriteLine("POST REPOSITORY");
            throw new NotImplementedException();
        }

        public Guid Update(Voluntary cliente)
        {
            Console.WriteLine("PUT REPOSITORY");
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;
using Npgsql;

namespace Ftec.WebAPI.Infra.Repository
{
    public class AffinityRepository : IAffinityRepository
    {
        
        private string connectionString;

        public AffinityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public Guid Insert(Affinity affinity)
        {
            throw new NotImplementedException();
        }

        public Affinity Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Affinity> FindAll()
        {
            List<Affinity> affinities = new List<Affinity>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM affinity";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Affinity affinity = new Affinity();
                    
                    affinity.AffinityId = Guid.Parse(reader["affinity_id"].ToString());
                    affinity.Name = reader["name"].ToString();
                    
                    affinities.Add(affinity);
                }
                
                return affinities;
            }
        }

        public Guid Update(Affinity voluntary)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
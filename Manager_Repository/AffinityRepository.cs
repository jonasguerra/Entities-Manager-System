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

        public Affinity Find(Guid id)
        {
            Affinity affinity = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM affinity WHERE affinity_id=@affinity_id";
                cmd.Parameters.AddWithValue("affinity_id", id.ToString());
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    affinity = new Affinity();
                    affinity.AffinityId = Guid.Parse(reader["affinity_id"].ToString());
                    affinity.Name = reader["name"].ToString();
                }
                return affinity;
            }
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
    }
}
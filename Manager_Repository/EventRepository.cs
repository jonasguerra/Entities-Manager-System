using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;
using Npgsql;

namespace Ftec.WebAPI.Infra.Repository
{
    public class EventRepository : IEventRepository
    {
        
        private string connectionString;

        public EventRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public Guid Insert(Event sevent)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        sevent.EventId = Guid.NewGuid();
                        sevent.Address.AddressId = Guid.NewGuid();
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        cmd.CommandText = @"INSERT Into address (address_id,cep,avenue,number,neighborhood,city,state)values(@address_id,@cep,@avenue,@number,@neighborhood,@city,@state)";
                        cmd.Parameters.AddWithValue("address_id", sevent.Address.AddressId); 
                        cmd.Parameters.AddWithValue("cep", sevent.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", sevent.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", sevent.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", sevent.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", sevent.Address.City); 
                        cmd.Parameters.AddWithValue("state", sevent.Address.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into event (event_id,title,description,date,address_id) values (@event_id,@title,@description,@date,@address_id)";
                        cmd.Parameters.AddWithValue("event_id", sevent.EventId);
                        cmd.Parameters.AddWithValue("title", sevent.Title);
                        cmd.Parameters.AddWithValue("description", sevent.Description); 
                        cmd.Parameters.AddWithValue("date", sevent.Date);
                        cmd.Parameters.AddWithValue("address_id", sevent.Address.AddressId);
                        cmd.ExecuteNonQuery();

                        foreach (var affinity in sevent.Affinities)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = @"INSERT Into event_affinity (event_id, affinity_id) VALUES (@event_id, @affinity_id)";
                            cmd.Parameters.AddWithValue("event_id", sevent.EventId);
                            cmd.Parameters.AddWithValue("affinity_id", affinity.AffinityId);
                            cmd.ExecuteNonQuery(); 
                        }
                        
                        //commit na transação
                        trans.Commit();
                        return sevent.EventId;

                    }
                    catch (Exception ex)
                    {
                        //rollback da transação
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public Event Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Event> FindByAffinityId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Event> FindAll()
        {
            throw new NotImplementedException();
        }

        public Guid Update(Event ssevent)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
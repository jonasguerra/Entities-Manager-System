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
        
        public Guid Insert(Event sEvent)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        sEvent.EventId = Guid.NewGuid();
                        sEvent.Address.AddressId = Guid.NewGuid();
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        cmd.CommandText = @"INSERT Into address (address_id,cep,avenue,number,neighborhood,city,state)values(@address_id,@cep,@avenue,@number,@neighborhood,@city,@state)";
                        cmd.Parameters.AddWithValue("address_id", sEvent.Address.AddressId); 
                        cmd.Parameters.AddWithValue("cep", sEvent.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", sEvent.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", sEvent.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", sEvent.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", sEvent.Address.City); 
                        cmd.Parameters.AddWithValue("state", sEvent.Address.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into event (event_id,title,description,date,address_id) values (@event_id,@title,@description,@date,@address_id)";
                        cmd.Parameters.AddWithValue("event_id", sEvent.EventId);
                        cmd.Parameters.AddWithValue("title", sEvent.Title);
                        cmd.Parameters.AddWithValue("description", sEvent.Description); 
                        cmd.Parameters.AddWithValue("date", sEvent.Date);
                        cmd.Parameters.AddWithValue("address_id", sEvent.Address.AddressId);
                        cmd.ExecuteNonQuery();

                        foreach (var affinity in sEvent.Affinities)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = @"INSERT Into event_affinity (event_id, affinity_id) VALUES (@event_id, @affinity_id)";
                            cmd.Parameters.AddWithValue("event_id", sEvent.EventId);
                            cmd.Parameters.AddWithValue("affinity_id", affinity.AffinityId);
                            cmd.ExecuteNonQuery(); 
                        }
                        
                        //commit na transação
                        trans.Commit();
                        return sEvent.EventId;

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

        public bool SetVoluntaryToEvent(Guid voluntaryId, Guid eventId)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;

                        cmd.CommandText = @"INSERT Into event_voluntary (event_id, voluntary_id) VALUES (@event_id, @voluntary_id)";
                        cmd.Parameters.AddWithValue("event_id", eventId);
                        cmd.Parameters.AddWithValue("voluntary_id", voluntaryId);
                        cmd.ExecuteNonQuery(); 
                        
                        trans.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
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

        public Guid Update(Event sEvent)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
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
            Event sEvent = null; 
            
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM event WHERE event_id=@event_id";
                cmd.Parameters.AddWithValue("event_id", id.ToString());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sEvent = new Event();
                    sEvent.EventId = Guid.Parse(reader["event_id"].ToString());
                    sEvent.Title =  reader["title"].ToString();
                    sEvent.Description = reader["Description"].ToString();
                    sEvent.Date = DateTime.Parse(reader["Date"].ToString());
                    sEvent.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                }
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM address WHERE address_id=@Id";
                cmd.Parameters.AddWithValue("Id", sEvent.Address.AddressId.ToString());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sEvent.Address.CEP = reader["cep"].ToString();
                    sEvent.Address.Avenue = reader["avenue"].ToString();
                    sEvent.Address.Number = reader["number"].ToString();
                    sEvent.Address.Neighborhood = reader["neighborhood"].ToString();
                    sEvent.Address.City = reader["city"].ToString();
                    sEvent.Address.State = reader["state"].ToString();
                }
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM affinity af join event_affinity av on af.affinity_id = av.affinity_id join event vo on vo.event_id = av.event_id WHERE av.event_id = @Id";
                cmd.Parameters.AddWithValue("Id", sEvent.EventId.ToString());
                reader = cmd.ExecuteReader();
                sEvent.Affinities = new List<Affinity>();
                while (reader.Read())
                {
                    sEvent.Affinities.Add(new Affinity()
                    {
                        AffinityId = Guid.Parse(reader["event_id"].ToString()),
                        Name = reader["name"].ToString()
                    });
                }

                return sEvent;
            }
        }

        public List<Event> FindByAffinityId(Guid id)
        {
            List<Event> events = new List<Event>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM event_affinity WHERE affinity_id=@affinity_id";
                cmd.Parameters.AddWithValue("affinity_id", id.ToString());
                var reader = cmd.ExecuteReader();
                
                List<string> event_id = new List<string>();
                
                while (reader.Read())
                {
                    event_id.Add(reader["event_id"].ToString());
                }
    
                foreach (string e_id in event_id)
                {
                    reader.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"SELECT * FROM event WHERE event_id=@Id";
                    cmd.Parameters.AddWithValue("Id", e_id);

                    Event sEvent= new Event();
                    
                    while (reader.Read())
                    {
                        sEvent.EventId = Guid.Parse(reader["event_id"].ToString());
                        sEvent.Title =  reader["title"].ToString();
                        sEvent.Description = reader["Description"].ToString();
                        sEvent.Date = DateTime.Parse(reader["Date"].ToString());
                        sEvent.Address = new Address()
                        {
                            AddressId = Guid.Parse(reader["address_id"].ToString())
                        };
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    cmd.CommandText = @"SELECT * FROM address WHERE address_id=@address_id";
                    cmd.Parameters.AddWithValue("address_id", sEvent.Address.AddressId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sEvent.Address.CEP = reader["cep"].ToString();
                        sEvent.Address.Avenue = reader["avenue"].ToString();
                        sEvent.Address.Number = reader["number"].ToString();
                        sEvent.Address.Neighborhood = reader["neighborhood"].ToString();
                        sEvent.Address.City = reader["city"].ToString();
                        sEvent.Address.State = reader["state"].ToString();
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    cmd.CommandText = @"SELECT af.name, af.affinity_id FROM affinity af 
                                        join event_affinity ea
                                        on af.affinity_id = ea.affinity_id 
                                        join event ev
                                        on ev.event_id = ea.event_id 
                                        WHERE ea.event_id = @Id";
                    
                    
                    cmd.Parameters.AddWithValue("Id", sEvent.EventId.ToString());
                    reader = cmd.ExecuteReader();
                    sEvent.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        sEvent.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["affinity_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                    events.Add(sEvent);
                }
            }

            return events;
        }

        public List<Event> FindAll()
        {
            List<Event> events = new List<Event>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM event";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Event sEvent = new Event();
                    sEvent.EventId = Guid.Parse(reader["event_id"].ToString());
                    sEvent.Title =  reader["title"].ToString();
                    sEvent.Description = reader["Description"].ToString();
                    sEvent.Date = DateTime.Parse(reader["Date"].ToString());
                    sEvent.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                    events.Add(sEvent);
                }
                
                foreach (Event sEvent in events)
                {   
                    reader.Close();
                    cmd.Parameters.Clear();
                
                    cmd.CommandText = @"SELECT * FROM address WHERE address_id=@address_id";
                    cmd.Parameters.AddWithValue("address_id", sEvent.Address.AddressId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sEvent.Address.CEP = reader["cep"].ToString();
                        sEvent.Address.Avenue = reader["avenue"].ToString();
                        sEvent.Address.Number = reader["number"].ToString();
                        sEvent.Address.Neighborhood = reader["neighborhood"].ToString();
                        sEvent.Address.City = reader["city"].ToString();
                        sEvent.Address.State = reader["state"].ToString();
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    cmd.CommandText = @"SELECT af.name, af.affinity_id FROM affinity af 
                                        join event_affinity ea
                                        on af.affinity_id = ea.affinity_id 
                                        join event ev
                                        on ev.event_id = ea.event_id 
                                        WHERE ea.event_id = @Id";
                    
                    
                    cmd.Parameters.AddWithValue("Id", sEvent.EventId.ToString());
                    reader = cmd.ExecuteReader();
                    sEvent.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        sEvent.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["affinity_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                }
                return events;
            }
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
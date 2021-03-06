using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;
using Npgsql;

namespace Ftec.WebAPI.Infra.Repository
{
    public class EntityRepository : IEntityRepository
    {
        private string connectionString;
        private UserRepository userRepository;
        public EntityRepository(string connectionString)
        {
            this.connectionString = connectionString;
            userRepository = new UserRepository(connectionString);
        }

        public bool Delete(Guid id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Entity entity = this.Find(id);
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;
                        
                        cmd.CommandText = @"DELETE FROM entity_affinity WHERE entity_id=@entity_id";
                        cmd.Parameters.AddWithValue("entity_id", entity.EntityId.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
                        cmd.CommandText = @"DELETE FROM entity WHERE entity_id=@entity_id";
                        cmd.Parameters.AddWithValue("entity_id", entity.EntityId.ToString());
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();

                        cmd.CommandText = @"DELETE FROM public.user WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", entity.UserId.ToString());
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();

                        cmd.CommandText = @"DELETE FROM address WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", entity.EntityAddress.AddressId.ToString());
                        cmd.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        Console.WriteLine(exception);
                        throw exception;
                    }
                }
            }
        }

        
        public Entity Find(Guid id)
        {
            Entity entity = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM entity WHERE entity_id=@entity_id";
                cmd.Parameters.AddWithValue("entity_id", id.ToString());

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    entity = new Entity();
                    entity.EntityId = Guid.Parse(reader["entity_id"].ToString());
                    entity.UserId = Guid.Parse(reader["user_id"].ToString());
                    entity.EntityName = reader["name"].ToString();
                    entity.EntityPhone = reader["phone"].ToString();
                    entity.EntityInitials = reader["sigla"].ToString();
                    entity.EntityDescription = reader["description"].ToString();
                    entity.EntityWebSite = reader["site"].ToString();
                    entity.EntityCreationDate = DateTime.Parse(reader["date_creation"].ToString());
                    entity.EntitySocialNetwork = reader["social_network"].ToString();
                    entity.EntityResponsableName = reader["responsable_name"].ToString();
                    entity.EntityAddress = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                }
              
                reader.Close();
                cmd.Parameters.Clear();
                
                User user = userRepository.Find(entity.UserId);
                entity.UserId = user.UserId;
                entity.IsApproved = user.IsApproved;
                entity.IsEntity = user.IsEntity;
                entity.IsVoluntary = user.IsVoluntary;
                entity.IsModerator = user.IsModerator;
                entity.Email = user.Email;
                entity.Password = user.Password;

                
                cmd.CommandText = @"SELECT * FROM address WHERE address_id=@Id";
                cmd.Parameters.AddWithValue("Id", entity.EntityAddress.AddressId.ToString());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entity.EntityAddress.CEP = reader["cep"].ToString();
                    entity.EntityAddress.Avenue = reader["avenue"].ToString();
                    entity.EntityAddress.Number = reader["number"].ToString();
                    entity.EntityAddress.Neighborhood = reader["neighborhood"].ToString();
                    entity.EntityAddress.City = reader["city"].ToString();
                    entity.EntityAddress.State = reader["state"].ToString();
                }
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM affinity af join entity_affinity av on af.affinity_id = av.affinity_id join entity vo on vo.entity_id = av.entity_id WHERE av.entity_id = @Id";
                cmd.Parameters.AddWithValue("Id", entity.EntityId.ToString());
                reader = cmd.ExecuteReader();
                entity.EntityAffinity= new List<Affinity>();
                while (reader.Read())
                {
                    entity.EntityAffinity.Add(new Affinity()
                    {
                        AffinityId = Guid.Parse(reader["entity_id"].ToString()),
                        Name = reader["name"].ToString()
                    });
                }
                return entity;
            }
        }


        public List<Entity> FindAll()
        {
           List<Entity> entities= new List<Entity>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM entity";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Entity entity= new Entity();
                    entity.EntityId = Guid.Parse(reader["entity_id"].ToString());
                    entity.UserId = Guid.Parse(reader["user_id"].ToString());
                    entity.EntityName = reader["name"].ToString();
                    entity.EntityResponsableName= reader["responsable_name"].ToString();
                    entity.EntityPhone= reader["phone"].ToString();
                    entity.EntityInitials= reader["sigla"].ToString();
                    entity.EntitySocialNetwork= reader["social_network"].ToString();
                    entity.EntityCreationDate= DateTime.Parse(reader["date_creation"].ToString());
                    entity.EntityWebSite= reader["site"].ToString();
                    entity.EntityDescription= reader["description"].ToString(); 
                    entity.EntityAddress = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                    
                    entities.Add(entity);
                }



                foreach (Entity entity in entities)
                {
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    User user = userRepository.Find(entity.UserId);
                    entity.UserId = user.UserId;
                    entity.IsApproved = user.IsApproved;
                    entity.IsEntity = user.IsEntity;
                    entity.IsVoluntary = user.IsVoluntary;
                    entity.IsModerator = user.IsModerator;
                    entity.Email = user.Email;
                    entity.Password = user.Password;
                    
                    cmd.CommandText = @"SELECT * FROM address WHERE address_id=@address_id";
                    cmd.Parameters.AddWithValue("address_id", entity.EntityAddress.AddressId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entity.EntityAddress.CEP = reader["cep"].ToString();
                        entity.EntityAddress.Avenue = reader["avenue"].ToString();
                        entity.EntityAddress.Number = reader["number"].ToString();
                        entity.EntityAddress.Neighborhood = reader["neighborhood"].ToString();
                        entity.EntityAddress.City = reader["city"].ToString();
                        entity.EntityAddress.State = reader["state"].ToString();
                    }
                    reader.Close();
                    cmd.Parameters.Clear();

                    
                    cmd.CommandText = @"SELECT af.name, af.affinity_id 
                                        FROM affinity af 
                                        join entity_affinity av on af.affinity_id = av.affinity_id 
                                        join entity vo on vo.entity_id = av.entity_id 
                                        WHERE av.entity_id = @Id";
                    cmd.Parameters.AddWithValue("Id", entity.EntityId.ToString());
                    reader = cmd.ExecuteReader();
                    entity.EntityAffinity= new List<Affinity>();
                    while (reader.Read())
                    {
                        entity.EntityAffinity.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["affinity_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                }
                return entities;
            }  
        }


        public Guid Insert(Entity entity)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        Guid userId = userRepository.Insert(new User()
                        {
                            UserId = entity.UserId,
                            IsApproved = entity.IsApproved,
                            IsEntity = entity.IsEntity,
                            IsVoluntary = entity.IsVoluntary,
                            IsModerator = entity.IsModerator,
                            Email = entity.Email,
                            Password = entity.Password
                        });
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into address (address_id, cep, avenue, number, neighborhood, city, state)values(@address_id, @cep, @avenue, @number, @neighborhood, @city, @state)";
                        cmd.Parameters.AddWithValue("address_id", entity.EntityAddress.AddressId); 
                        cmd.Parameters.AddWithValue("cep", entity.EntityAddress.CEP); 
                        cmd.Parameters.AddWithValue("avenue", entity.EntityAddress.Avenue); 
                        cmd.Parameters.AddWithValue("number", entity.EntityAddress.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", entity.EntityAddress.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", entity.EntityAddress.City); 
                        cmd.Parameters.AddWithValue("state", entity.EntityAddress.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into entity (entity_id, name, responsable_name, phone, sigla, social_network, date_creation, site, description, user_id, address_id) values (@entity_id, @name, @responsable_name, @phone, @sigla, @social_network, @date_creation, @site, @description, @user_id, @address_id)";
                        cmd.Parameters.AddWithValue("entity_id", entity.EntityId);
                        cmd.Parameters.AddWithValue("name", entity.EntityName);
                        cmd.Parameters.AddWithValue("responsable_name", entity.EntityResponsableName); 
                        cmd.Parameters.AddWithValue("phone", entity.EntityPhone);
                        cmd.Parameters.AddWithValue("sigla", entity.EntityInitials);
                        cmd.Parameters.AddWithValue("social_network", entity.EntitySocialNetwork);
                        cmd.Parameters.AddWithValue("date_creation", entity.EntityCreationDate);
                        cmd.Parameters.AddWithValue("description", entity.EntityDescription);
                        cmd.Parameters.AddWithValue("site", entity.EntityWebSite);
                        cmd.Parameters.AddWithValue("user_id", entity.UserId);
                        cmd.Parameters.AddWithValue("address_id", entity.EntityAddress.AddressId);
                        cmd.ExecuteNonQuery();
                       

                        foreach (var affinity in entity.EntityAffinity)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = @"INSERT Into entity_affinity (entity_id, affinity_id) VALUES (@entity_id, @affinity_id)";
                            cmd.Parameters.AddWithValue("entity_id", entity.EntityId);
                            cmd.Parameters.AddWithValue("affinity_id", affinity.AffinityId); 
                            cmd.ExecuteNonQuery(); 
                        }
                        
                        //commit na transação
                        trans.Commit();
                        return entity.EntityId;

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

         
        public Guid Update(Entity entity)
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
                      
                        Guid userId = userRepository.Update(new User()
                        {
                            UserId = entity.UserId,
                            Email = entity.Email,
                            Password = entity.Password,
                            IsApproved = entity.IsApproved,
                            IsEntity = entity.IsEntity
                            
                        });
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE address SET cep = @cep, avenue = @avenue, number = @number, neighborhood = @neighborhood, city = @city, state = @state WHERE address_id = @address_id";
                        cmd.Parameters.AddWithValue("address_id", entity.EntityAddress.AddressId.ToString()); 
                        cmd.Parameters.AddWithValue("cep", entity.EntityAddress.CEP); 
                        cmd.Parameters.AddWithValue("avenue", entity.EntityAddress.Avenue); 
                        cmd.Parameters.AddWithValue("number", entity.EntityAddress.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", entity.EntityAddress.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", entity.EntityAddress.City); 
                        cmd.Parameters.AddWithValue("state", entity.EntityAddress.State); 
                        cmd.ExecuteNonQuery();
                        
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE entity SET name = @name, responsable_name = @responsable_name, phone = @phone, sigla = @sigla,  social_network = @social_network, date_creation = @date_creation, site = @site WHERE entity_id = @entity_id";
                        cmd.Parameters.AddWithValue("entity_id", entity.EntityId.ToString());
                        cmd.Parameters.AddWithValue("name", entity.EntityName);
                        cmd.Parameters.AddWithValue("responsable_name", entity.EntityResponsableName); 
                        cmd.Parameters.AddWithValue("phone", entity.EntityPhone);
                        cmd.Parameters.AddWithValue("sigla", entity.EntityInitials);
                        cmd.Parameters.AddWithValue("social_network", entity.EntitySocialNetwork);
                        cmd.Parameters.AddWithValue("date_creation", entity.EntityCreationDate);
                        cmd.Parameters.AddWithValue("site", entity.EntityWebSite);
                        cmd.ExecuteNonQuery();
                        
                        trans.Commit();
                        return entity.EntityId;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
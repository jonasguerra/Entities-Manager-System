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

        public EntityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool Delete(Guid id)
        {
            Console.WriteLine("DELETE Repository - Entity");
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Entity entity = this.Find(id);
                        Console.WriteLine(entity.EntityId);

                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;
                        cmd.CommandText = @"DELETE FROM entity WHERE entity_id=@entity_id";
                        cmd.Parameters.AddWithValue("entity_id", entity.EntityId.ToString());
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();

                        cmd.CommandText = @"DELETE FROM public.user WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", entity.UserId.ToString());
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();

                        cmd.CommandText = @"DELETE FROM address WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", entity.EntityAddress.AddressId);
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
            Console.WriteLine("GET ONE ENTITY - Repository");
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
                    entity.EntityId = Guid.Parse(reader["voluntary_id"].ToString());
                    entity.UserId = Guid.Parse(reader["user_id"].ToString());
                    entity.EntityName = reader["Name"].ToString();
                    entity.EntityEmail = reader["Email"].ToString();
                    entity.EntityPhone = reader["Phone"].ToString();
                    entity.EntityInitials = reader["Initials"].ToString();
                    entity.EntityDescription = reader["Description"].ToString();
                    entity.EntityWebSite = reader["Website"].ToString();
                    entity.EntityCreationDate = DateTime.Parse(reader["CreationDate"].ToString());
                    entity.EntitySocialNetwork = reader["SocialNetwork"].ToString();
                    entity.EntityResponsableName = reader["ResposableName"].ToString();
                    entity.EntityAddress = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                    
                    
                    
                }
                
                //TODO aqui tem que colocar a leitura das afinidades, que pode ser mais de 1
                
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                cmd.Parameters.AddWithValue("user_id", entity.UserId.ToString());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entity.IsApproved = (bool) reader["is_approved"];
                    entity.IsEntity = (bool) reader["is_entity"];
                    entity.Email = reader["email"].ToString();
                    entity.Password = reader["password"].ToString();
                }

                reader.Close();
                cmd.Parameters.Clear();

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

                return entity;
            }
        }

        public List<Entity> FindAll()
        {
            throw new NotImplementedException();
        }

        public Guid Insert(Entity entity)
        {
            Console.WriteLine("POST METHOD 5");
            
            throw new NotImplementedException();
        }

        public Guid Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
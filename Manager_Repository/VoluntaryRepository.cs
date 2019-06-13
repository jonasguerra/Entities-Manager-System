using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;
using Npgsql;

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
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        Voluntary voluntary = this.Find(id);
                        
                        Console.WriteLine(voluntary.VoluntaryId);
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        cmd.CommandText = @"DELETE FROM voluntary_affinity WHERE voluntary_id=@voluntary_id";
                        cmd.Parameters.AddWithValue("voluntary_id", voluntary.VoluntaryId.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
                        cmd.CommandText = @"DELETE FROM voluntary WHERE voluntary_id=@voluntary_id";
                        cmd.Parameters.AddWithValue("voluntary_id", voluntary.VoluntaryId.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
                        cmd.CommandText = @"DELETE FROM public.user WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", voluntary.UserId.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
                        cmd.CommandText = @"DELETE FROM address WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", voluntary.Address.ToString());
                        cmd.ExecuteNonQuery();
                        
                        trans.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        Console.WriteLine(ex);
                        throw ex;
                    }
                }
            }
        }

        public Voluntary Find(Guid id)
        {
            Console.WriteLine("GET ONE REPOSITORY");
            Voluntary voluntary = null;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM voluntary WHERE voluntary_id=@voluntary_id";
                cmd.Parameters.AddWithValue("voluntary_id", id.ToString());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    voluntary = new Voluntary();
                    voluntary.VoluntaryId = Guid.Parse(reader["voluntary_id"].ToString());
                    voluntary.UserId =  Guid.Parse(reader["user_id"].ToString());
                    voluntary.Name = reader["Name"].ToString();
                    voluntary.Phone = reader["Phone"].ToString();
                    voluntary.SocialNetwork = reader["SocialNetwork"].ToString();
                    voluntary.PhotoImageName = reader["PhotoImageName"].ToString();
                    voluntary.PhotoImageName = reader["PhotoImageName"].ToString();
                    voluntary.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                }
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                cmd.Parameters.AddWithValue("user_id", voluntary.UserId.ToString());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    voluntary.IsApproved = (bool)reader["is_approved"];
                    voluntary.IsEntity = (bool)reader["is_entity"];
                    voluntary.Email = reader["email"].ToString();
                    voluntary.Password = reader["password"].ToString();
                }
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM address WHERE address_id=@Id";
                cmd.Parameters.AddWithValue("Id", voluntary.Address.AddressId.ToString());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    voluntary.Address.CEP = reader["cep"].ToString();
                    voluntary.Address.Avenue = reader["avenue"].ToString();
                    voluntary.Address.Number = reader["number"].ToString();
                    voluntary.Address.Neighborhood = reader["neighborhood"].ToString();
                    voluntary.Address.City = reader["city"].ToString();
                    voluntary.Address.State = reader["state"].ToString();
                }
                
                return voluntary;
            }
        }

        public Voluntary FindByAffinityId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Voluntary> FindAll()
        {
            Console.WriteLine("GET ALL REPOSITORY");
            
            List<Voluntary> volunteers = new List<Voluntary>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM voluntary";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Voluntary voluntary = new Voluntary();
                    voluntary.VoluntaryId = Guid.Parse(reader["voluntary_id"].ToString());
                    voluntary.UserId =  Guid.Parse(reader["user_id"].ToString());
                    voluntary.Name = reader["name"].ToString();
                    voluntary.Phone = reader["phone"].ToString();
                    voluntary.SocialNetwork = reader["socialnetwork"].ToString();
                    voluntary.PhotoImageName = reader["photoImageName"].ToString();
                    voluntary.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };

                    volunteers.Add(voluntary);
                }
                
                foreach (Voluntary voluntary in volunteers)
                {   
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                    cmd.Parameters.AddWithValue("user_id", voluntary.UserId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        voluntary.IsApproved = (bool)reader["is_approved"];
                        voluntary.IsEntity = (bool)reader["is_entity"];
                        voluntary.Email = reader["email"].ToString();
                        voluntary.Password = reader["password"].ToString();
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                
                    cmd.CommandText = @"SELECT * FROM address WHERE address_id=@address_id";
                    cmd.Parameters.AddWithValue("address_id", voluntary.Address.AddressId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        voluntary.Address.CEP = reader["cep"].ToString();
                        voluntary.Address.Avenue = reader["avenue"].ToString();
                        voluntary.Address.Number = reader["number"].ToString();
                        voluntary.Address.Neighborhood = reader["neighborhood"].ToString();
                        voluntary.Address.City = reader["city"].ToString();
                        voluntary.Address.State = reader["state"].ToString();
                    }
                }
                return volunteers;
            }
        }

        public Guid Insert(Voluntary voluntary)
        {
            Console.WriteLine("POST REPOSITORY");
            
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                //Inicia a transação
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        voluntary.PhotoImageName = "/photo";
                        voluntary.VoluntaryId = Guid.NewGuid();
                        voluntary.UserId = Guid.NewGuid();
                        voluntary.Address.AddressId = Guid.NewGuid();
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        
                        cmd.CommandText = @"INSERT Into public.user (user_id,email,password,is_approved,is_entity)values(@user_id,@email,@password,@is_approved,@is_entity)";
                        cmd.Parameters.AddWithValue("user_id", voluntary.UserId);
                        cmd.Parameters.AddWithValue("email", voluntary.Email);
                        cmd.Parameters.AddWithValue("password", voluntary.Password); 
                        cmd.Parameters.AddWithValue("is_approved", voluntary.IsApproved);
                        cmd.Parameters.AddWithValue("is_entity", voluntary.IsEntity);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into address (address_id,cep,avenue,number,neighborhood,city,state)values(@address_id,@cep,@avenue,@number,@neighborhood,@city,@state)";
                        cmd.Parameters.AddWithValue("address_id", voluntary.Address.AddressId); 
                        cmd.Parameters.AddWithValue("cep", voluntary.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", voluntary.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", voluntary.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", voluntary.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", voluntary.Address.City); 
                        cmd.Parameters.AddWithValue("state", voluntary.Address.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into voluntary (voluntary_id,name,phone,affinity,socialnetwork,photoimagename,user_id,address_id) values (@voluntary_id,@name,@phone,@affinity,@socialnetwork,@photoimagename,@user_id,@address_id)";
                        cmd.Parameters.AddWithValue("voluntary_id", voluntary.VoluntaryId);
                        cmd.Parameters.AddWithValue("name", voluntary.Name);
                        cmd.Parameters.AddWithValue("phone", voluntary.Phone); 
                        cmd.Parameters.AddWithValue("socialnetwork", voluntary.SocialNetwork);
                        cmd.Parameters.AddWithValue("photoimagename", voluntary.PhotoImageName);
                        cmd.Parameters.AddWithValue("user_id", voluntary.UserId);
                        cmd.Parameters.AddWithValue("address_id", voluntary.Address.AddressId);
                        cmd.ExecuteNonQuery();


                        foreach (var affinity in voluntary.Affinities)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = @"INSERT Into voluntary_affinity (voluntary_id, affinity_id) VALUES (@voluntary_id, @affinity_id)";
                            cmd.Parameters.AddWithValue("voluntary_id", voluntary.VoluntaryId);
                            cmd.Parameters.AddWithValue("affinity_id", affinity.AffinityId);
                            cmd.ExecuteNonQuery(); 
                        }
                        
                        
                        //commit na transação
                        trans.Commit();
                        return voluntary.VoluntaryId;

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

        public Guid Update(Voluntary voluntary)
        {
            Console.WriteLine("PUT REPOSITORY");
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        voluntary.PhotoImageName = "here";
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        
                        cmd.CommandText = @"UPDATE voluntary SET name=@name,phone=@phone,affinity=@affinity,socialnetwork=@socialnetwork,photoimagename=@photoimagename WHERE voluntary_id=@voluntary_id";
                        cmd.Parameters.AddWithValue("name", voluntary.Name);
                        cmd.Parameters.AddWithValue("phone", voluntary.Phone); 
                        cmd.Parameters.AddWithValue("socialnetwork", voluntary.SocialNetwork);
                        cmd.Parameters.AddWithValue("photoimagename", voluntary.PhotoImageName);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE address SET cep=@cep,avenue=@avenue,number=@number,neighborhood=@neighborhood,city=@city,state=@state WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("cep", voluntary.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", voluntary.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", voluntary.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", voluntary.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", voluntary.Address.City); 
                        cmd.Parameters.AddWithValue("state", voluntary.Address.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE public.user SET email=@email,password=@password,is_approved=@is_approved WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("email", voluntary.Email);
                        cmd.Parameters.AddWithValue("password", voluntary.Password); 
                        cmd.Parameters.AddWithValue("is_approved", voluntary.IsApproved);
                        cmd.Parameters.AddWithValue("is_entity", voluntary.IsEntity);
                        cmd.ExecuteNonQuery();
                        
                        trans.Commit();
                        return voluntary.VoluntaryId;

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
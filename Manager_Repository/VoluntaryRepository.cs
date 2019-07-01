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
        private UserRepository userRepository;
        
        
        public VoluntaryRepository(string connectionString)
        {
            this.connectionString = connectionString;
            userRepository = new UserRepository(connectionString);
        }

        public bool Delete(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        Voluntary voluntary = this.Find(id);
                        
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

                        cmd.CommandText = @"DELETE FROM address WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", voluntary.Address.ToString());
                        cmd.ExecuteNonQuery();
                        
                        trans.Commit();
                        
                        userRepository.Delete(voluntary.UserId);
                        
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

        public Voluntary Find(Guid id)
        {
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
                    voluntary.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                }
                reader.Close();
                cmd.Parameters.Clear();

                User user = userRepository.Find(voluntary.UserId);
                voluntary.UserId = user.UserId;
                voluntary.IsApproved = user.IsApproved;
                voluntary.IsEntity = user.IsEntity;
                voluntary.IsVoluntary = user.IsVoluntary;
                voluntary.IsModerator = user.IsModerator;
                voluntary.Email = user.Email;
                voluntary.Password = user.Password;
                
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
                reader.Close();
                cmd.Parameters.Clear();
                
                cmd.CommandText = @"SELECT * FROM affinity af join voluntary_affinity av on af.affinity_id = av.affinity_id join voluntary vo on vo.voluntary_id = av.voluntary_id WHERE av.voluntary_id = @Id";
                cmd.Parameters.AddWithValue("Id", voluntary.VoluntaryId.ToString());
                reader = cmd.ExecuteReader();
                voluntary.Affinities = new List<Affinity>();
                while (reader.Read())
                {
                    voluntary.Affinities.Add(new Affinity()
                    {
                        AffinityId = Guid.Parse(reader["voluntary_id"].ToString()),
                        Name = reader["name"].ToString()
                    });
                }

                return voluntary;
            }
        }

        public List<Voluntary> FindByAffinityId(Guid id)
        {
            List<Voluntary> volunteers = new List<Voluntary>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM voluntary_affinity WHERE affinity_id=@affinity_id";
                cmd.Parameters.AddWithValue("affinity_id", id.ToString());
                var reader = cmd.ExecuteReader();
                
                List<string> voluntary_id = new List<string>();
                
                while (reader.Read())
                {
                    voluntary_id.Add(reader["voluntary_id"].ToString());
                }
    
                foreach (string v_id in voluntary_id)
                {
                    reader.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"SELECT * FROM voluntary WHERE voluntary_id=@Id";
                    cmd.Parameters.AddWithValue("Id", v_id);

                    Voluntary voluntary = new Voluntary();
                    
                    while (reader.Read())
                    {
                        voluntary = new Voluntary();
                        voluntary.VoluntaryId = Guid.Parse(reader["voluntary_id"].ToString());
                        voluntary.UserId =  Guid.Parse(reader["user_id"].ToString());
                        voluntary.Name = reader["Name"].ToString();
                        voluntary.Phone = reader["Phone"].ToString();
                        voluntary.SocialNetwork = reader["SocialNetwork"].ToString();
                        voluntary.Address = new Address()
                        {
                            AddressId = Guid.Parse(reader["address_id"].ToString())
                        };
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    User user = userRepository.Find(voluntary.UserId);
                    voluntary.UserId = user.UserId;
                    voluntary.IsApproved = user.IsApproved;
                    voluntary.IsEntity = user.IsEntity;
                    voluntary.IsVoluntary = user.IsVoluntary;
                    voluntary.IsModerator = user.IsModerator;
                    voluntary.Email = user.Email;
                    voluntary.Password = user.Password;
                    
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
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    cmd.CommandText = @"SELECT * FROM affinity af join voluntary_affinity av on af.affinity_id = av.affinity_id join voluntary vo on vo.voluntary_id = av.voluntary_id WHERE av.voluntary_id = @Id";
                    cmd.Parameters.AddWithValue("Id", voluntary.VoluntaryId.ToString());
                    reader = cmd.ExecuteReader();
                    voluntary.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        voluntary.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["voluntary_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                    volunteers.Add(voluntary);
                }
            }

            return volunteers;
        }

        public List<Voluntary> FindAll()
        {
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
                    
                    User user = userRepository.Find(voluntary.UserId);
                    voluntary.UserId = user.UserId;
                    voluntary.IsApproved = user.IsApproved;
                    voluntary.IsEntity = user.IsEntity;
                    voluntary.IsVoluntary = user.IsVoluntary;
                    voluntary.IsModerator = user.IsModerator;
                    voluntary.Email = user.Email;
                    voluntary.Password = user.Password;
                
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
                    reader.Close();
                    cmd.Parameters.Clear();
                    
                    cmd.CommandText = @"SELECT af.name, af.affinity_id FROM affinity af join voluntary_affinity av on af.affinity_id = av.affinity_id join voluntary vo on vo.voluntary_id = av.voluntary_id WHERE av.voluntary_id = @Id";
                    cmd.Parameters.AddWithValue("Id", voluntary.VoluntaryId.ToString());
                    reader = cmd.ExecuteReader();
                    voluntary.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        voluntary.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["affinity_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                }
                return volunteers;
            }
        }

        public Guid Insert(Voluntary voluntary)
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
                            UserId = voluntary.UserId,
                            IsApproved = voluntary.IsApproved,
                            IsEntity = voluntary.IsEntity,
                            IsVoluntary = voluntary.IsVoluntary,
                            IsModerator = voluntary.IsModerator,
                            Email = voluntary.Email,
                            Password = voluntary.Password
                        });
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
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
                        cmd.CommandText = @"INSERT Into voluntary (voluntary_id,name,phone,socialnetwork,user_id,address_id) values (@voluntary_id,@name,@phone,@socialnetwork,@user_id,@address_id)";
                        cmd.Parameters.AddWithValue("voluntary_id", voluntary.VoluntaryId);
                        cmd.Parameters.AddWithValue("name", voluntary.Name);
                        cmd.Parameters.AddWithValue("phone", voluntary.Phone); 
                        cmd.Parameters.AddWithValue("socialnetwork", voluntary.SocialNetwork);
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
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        
                        Guid userId = userRepository.Update(new User()
                        {
                            UserId = voluntary.UserId,
                            IsApproved = voluntary.IsApproved,
                            IsEntity = voluntary.IsEntity,
                            IsVoluntary = voluntary.IsVoluntary,
                            IsModerator = voluntary.IsModerator,
                            Email = voluntary.Email,
                            Password = voluntary.Password
                        });
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                         
                        cmd.CommandText = @"UPDATE voluntary SET name = @name, socialnetwork = @socialnetwork WHERE voluntary_id = @voluntary_id";
                        cmd.Parameters.AddWithValue("voluntary_id", voluntary.VoluntaryId.ToString());
                        cmd.Parameters.AddWithValue("name", voluntary.Name);
                        cmd.Parameters.AddWithValue("phone", voluntary.Phone); 
                        cmd.Parameters.AddWithValue("socialnetwork", voluntary.SocialNetwork);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE address SET cep=@cep,avenue=@avenue,number=@number,neighborhood=@neighborhood,city=@city,state=@state WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", voluntary.Address.AddressId.ToString());
                        cmd.Parameters.AddWithValue("cep", voluntary.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", voluntary.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", voluntary.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", voluntary.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", voluntary.Address.City); 
                        cmd.Parameters.AddWithValue("state", voluntary.Address.State); 
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
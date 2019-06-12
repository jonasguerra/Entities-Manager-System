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
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"DELETE FROM user WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("Id", id.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
                        cmd.CommandText = @"DELETE FROM voluntary WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("Id", id.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
                        cmd.CommandText = @"DELETE FROM voluntary WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("Id", id.ToString());
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

        public Voluntary Find(Guid id)
        {
            Console.WriteLine("GET ONE REPOSITORY");
            Voluntary voluntary = null;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM voluntary WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", id.ToString());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    voluntary = new Voluntary();
                    voluntary.Id = Guid.Parse(reader["Id"].ToString());
                    voluntary.IsApproved = (bool)reader["IsApproved"];
                    voluntary.Name = reader["Name"].ToString();
                    voluntary.Email = reader["Email"].ToString();
                    voluntary.Phone = reader["Phone"].ToString();
                    voluntary.Password = reader["Password"].ToString();
                    voluntary.CEP = reader["CEP"].ToString();
                    voluntary.Avenue = reader["Avenue"].ToString();
                    voluntary.Number = reader["Number"].ToString();
                    voluntary.Neighborhood = reader["Neighborhood"].ToString();
                    voluntary.City = reader["City"].ToString();
                    voluntary.State = reader["State"].ToString();
                    voluntary.Affinity = reader["Affinity"].ToString();
                    voluntary.SocialNetwork = reader["SocialNetwork"].ToString();
                    voluntary.PhotoImageName = reader["PhotoImageName"].ToString();
                }

                reader.Close();
                return voluntary;
            }
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
                    voluntary.Id = Guid.Parse(reader["Id"].ToString());
                    voluntary.IsApproved = (bool)reader["IsApproved"];
                    voluntary.Name = reader["Name"].ToString();
                    voluntary.Email = reader["Email"].ToString();
                    voluntary.Phone = reader["Phone"].ToString();
                    voluntary.Password = reader["Password"].ToString();
                    voluntary.CEP = reader["CEP"].ToString();
                    voluntary.Avenue = reader["Avenue"].ToString();
                    voluntary.Number = reader["Number"].ToString();
                    voluntary.Neighborhood = reader["Neighborhood"].ToString();
                    voluntary.City = reader["City"].ToString();
                    voluntary.State = reader["State"].ToString();
                    voluntary.Affinity = reader["Affinity"].ToString();
                    voluntary.SocialNetwork = reader["SocialNetwork"].ToString();
                    voluntary.PhotoImageName = reader["PhotoImageName"].ToString();

                    volunteers.Add(voluntary);
                }

                reader.Close();
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
                        voluntary.Id = Guid.NewGuid();
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"INSERT Into voluntary (Id, IsApproved, Name, Email, Phone, Password, CEP, Avenue, Number, Neighborhood, City, State, Affinity, SocialNetwork, PhotoImageName ) values(@Id, @IsApproved, @Name, @Email, @Phone, @Password, @CEP, @Avenue, @Number, @Neighborhood, @City, @State, @Affinity, @SocialNetwork, @PhotoImageName)";
                        cmd.Parameters.AddWithValue("Id", voluntary.Id);
                        cmd.Parameters.AddWithValue("Name", voluntary.Name);
                        cmd.Parameters.AddWithValue("IsApproved", voluntary.IsApproved);
                        cmd.Parameters.AddWithValue("Email", voluntary.Email); 
                        cmd.Parameters.AddWithValue("Phone", voluntary.Phone); 
                        cmd.Parameters.AddWithValue("Password", voluntary.Password); 
                        cmd.Parameters.AddWithValue("CEP", voluntary.CEP); 
                        cmd.Parameters.AddWithValue("Avenue", voluntary.Avenue); 
                        cmd.Parameters.AddWithValue("Number", voluntary.Number); 
                        cmd.Parameters.AddWithValue("Neighborhood", voluntary.Neighborhood); 
                        cmd.Parameters.AddWithValue("City", voluntary.City); 
                        cmd.Parameters.AddWithValue("State", voluntary.State); 
                        cmd.Parameters.AddWithValue("Affinity", voluntary.Affinity); 
                        cmd.Parameters.AddWithValue("SocialNetwork", voluntary.SocialNetwork);
                        cmd.Parameters.AddWithValue("PhotoImageName", voluntary.PhotoImageName);
                        cmd.ExecuteNonQuery();

                        //commit na transação
                        trans.Commit();
                        return voluntary.Id;

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
                        cmd.CommandText = @"UPDATE voluntary SET Id=@Id,IsApproved=@IsApproved,Name=@Name,Email=@Email,Phone=@Phone,Password=@Password,CEP=@CEP,Avenue=@Avenue,Number=@Number,Neighborhood=@Neighborhood,City=@City,State=@State,Affinity=@Affinity,SocialNetwork=@SocialNetwork,PhotoImageName=@PhotoImageName WHERE Id=@id";
                        cmd.Parameters.AddWithValue("Id", voluntary.Id);
                        cmd.Parameters.AddWithValue("Name", voluntary.Name);
                        cmd.Parameters.AddWithValue("IsApproved", voluntary.IsApproved);
                        cmd.Parameters.AddWithValue("Email", voluntary.Email); 
                        cmd.Parameters.AddWithValue("Phone", voluntary.Phone); 
                        cmd.Parameters.AddWithValue("Password", voluntary.Password); 
                        cmd.Parameters.AddWithValue("CEP", voluntary.CEP); 
                        cmd.Parameters.AddWithValue("Avenue", voluntary.Avenue); 
                        cmd.Parameters.AddWithValue("Number", voluntary.Number); 
                        cmd.Parameters.AddWithValue("Neighborhood", voluntary.Neighborhood); 
                        cmd.Parameters.AddWithValue("City", voluntary.City); 
                        cmd.Parameters.AddWithValue("State", voluntary.State); 
                        cmd.Parameters.AddWithValue("Affinity", voluntary.Affinity); 
                        cmd.Parameters.AddWithValue("SocialNetwork", voluntary.SocialNetwork);
                        cmd.Parameters.AddWithValue("PhotoImageName", voluntary.PhotoImageName);
                        cmd.ExecuteNonQuery();

                        
                        trans.Commit();
                        return voluntary.Id;

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
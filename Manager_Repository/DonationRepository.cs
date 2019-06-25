using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;
using Npgsql;

namespace Ftec.WebAPI.Infra.Repository
{
    public class DonationRepository : IDonationRepository
    {
        private string connectionString;

        public DonationRepository(string connectionString)
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
                        Donation donation = this.Find(id);
                        
                        Console.WriteLine(donation.DonationId);
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        cmd.CommandText = @"DELETE FROM donation WHERE donation_id=@donation_id";
                        cmd.Parameters.AddWithValue("donation_id", donation.DonationId.ToString());
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        
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

        public Donation Find(Guid id)
        {
            Console.WriteLine("FIND ONE - REPOSITORY");
            Donation donation = null;
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM donation WHERE donation_id=@donation_id";
                cmd.Parameters.AddWithValue("donation_id", id.ToString());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    donation = new Donation();
                    donation.DonationId = Guid.Parse(reader["donation_id"].ToString());
                    donation.Title = reader["Title"].ToString();
                    donation.Description = reader["Description"].ToString();
                    donation.Quantity = reader["Quantity"].ToString();
                    donation.takeDonation = (bool)reader["TakeDonation"];
                    
                    donation.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        donation.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["donation_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                    
                    donation.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                }
                reader.Close();
                cmd.Parameters.Clear();

                return donation;
            }
        }

        public List<Donation> FindByAffinityId(Guid id)
        {
            List<Donation> volunteers = new List<Donation>();

            Console.WriteLine("GET ONE REPOSITORY");
            
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM voluntary_affinity WHERE affinity_id=@affinity_id";
                cmd.Parameters.AddWithValue("affinity_id", id.ToString());
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                }
            }

            return volunteers;
        }

        public List<Donation> FindAll()
        {
            Console.WriteLine("GET ALL REPOSITORY");
            
            List<Donation> donations = new List<Donation>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM donation";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Donation donation = new Donation();
                    donation.DonationId = Guid.Parse(reader["donation_id"].ToString());
                    donation.Title = reader["Title"].ToString();
                    donation.Description = reader["Description"].ToString();
                    donation.Quantity = reader["Quantity"].ToString();
                    donation.takeDonation = (bool)reader["TakeDonation"];
                    
                    donation.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        donation.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["donation_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }
                    
                    donation.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                    
                    donations.Add(donation);
                }
                
               
                return donations;
            }
        }
        
        //falta fazer daqui pra baixo ********************************************************************************************************************************************
        
        public Guid Insert(Donation donation)
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
                        donation.DonationId = Guid.NewGuid();
                        donation.UserId = Guid.NewGuid();
                        donation.Address.AddressId = Guid.NewGuid();
                        
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        
                        
                        cmd.CommandText = @"INSERT Into public.user (user_id,email,password,is_approved,is_voluntary)values(@user_id,@email,@password,@is_approved,@is_voluntary)";
                        cmd.Parameters.AddWithValue("user_id", donation.UserId);
                        cmd.Parameters.AddWithValue("email", donation.Email);
                        cmd.Parameters.AddWithValue("password", donation.Password); 
                        cmd.Parameters.AddWithValue("is_approved", donation.IsApproved);
                        cmd.Parameters.AddWithValue("is_voluntary", donation.IsVoluntary);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into address (address_id,cep,avenue,number,neighborhood,city,state)values(@address_id,@cep,@avenue,@number,@neighborhood,@city,@state)";
                        cmd.Parameters.AddWithValue("address_id", donation.Address.AddressId); 
                        cmd.Parameters.AddWithValue("cep", donation.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", donation.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", donation.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", donation.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", donation.Address.City); 
                        cmd.Parameters.AddWithValue("state", donation.Address.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"INSERT Into donation (donation_id,name,phone,socialnetwork,photoimagename,user_id,address_id) values (@donation_id,@name,@phone,@socialnetwork,@photoimagename,@user_id,@address_id)";
                        cmd.Parameters.AddWithValue("donation_id", donation.DonationId);
                        cmd.Parameters.AddWithValue("name", donation.Name);
                        cmd.Parameters.AddWithValue("phone", donation.Phone); 
                        cmd.Parameters.AddWithValue("socialnetwork", donation.SocialNetwork);
                        cmd.Parameters.AddWithValue("photoimagename", donation.PhotoImageName);
                        cmd.Parameters.AddWithValue("user_id", donation.UserId);
                        cmd.Parameters.AddWithValue("address_id", donation.Address.AddressId);
                        cmd.ExecuteNonQuery();


                        foreach (var affinity in donation.Affinities)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = @"INSERT Into voluntary_affinity (donation_id, affinity_id) VALUES (@donation_id, @affinity_id)";
                            cmd.Parameters.AddWithValue("donation_id", donation.DonationId);
                            cmd.Parameters.AddWithValue("affinity_id", affinity.AffinityId);
                            cmd.ExecuteNonQuery(); 
                        }
                        
                        //commit na transação
                        trans.Commit();
                        return donation.DonationId;

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

        public Guid Update(Donation donation)
        {
            Console.WriteLine("PUT REPOSITORY");
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
                         
                        cmd.CommandText = @"UPDATE donation SET name = @name, socialnetwork = @socialnetwork, photoimagename = @photoimagename WHERE donation_id = @donation_id";
                        cmd.Parameters.AddWithValue("donation_id", donation.DonationId.ToString());
                        cmd.Parameters.AddWithValue("name", donation.Name);
                        cmd.Parameters.AddWithValue("phone", donation.Phone); 
                        cmd.Parameters.AddWithValue("socialnetwork", donation.SocialNetwork);
                        cmd.Parameters.AddWithValue("photoimagename", donation.PhotoImageName);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE address SET cep=@cep,avenue=@avenue,number=@number,neighborhood=@neighborhood,city=@city,state=@state WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", donation.Address.AddressId.ToString());
                        cmd.Parameters.AddWithValue("cep", donation.Address.CEP); 
                        cmd.Parameters.AddWithValue("avenue", donation.Address.Avenue); 
                        cmd.Parameters.AddWithValue("number", donation.Address.Number); 
                        cmd.Parameters.AddWithValue("neighborhood", donation.Address.Neighborhood); 
                        cmd.Parameters.AddWithValue("city", donation.Address.City); 
                        cmd.Parameters.AddWithValue("state", donation.Address.State); 
                        cmd.ExecuteNonQuery();
                        
                        cmd.Parameters.Clear();
                        cmd.CommandText = @"UPDATE public.user SET email=@email,password=@password,is_approved=@is_approved WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", donation.UserId.ToString());
                        cmd.Parameters.AddWithValue("email", donation.Email);
                        cmd.Parameters.AddWithValue("password", donation.Password); 
                        cmd.Parameters.AddWithValue("is_approved", donation.IsApproved);
                        cmd.ExecuteNonQuery();
                        
                        trans.Commit();
                        return donation.DonationId;

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
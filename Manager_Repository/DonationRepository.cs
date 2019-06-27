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
                    donation.TakeDonation = (bool) reader["TakeDonation"];

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
                    donation.TakeDonation = (bool) reader["TakeDonation"];

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


                        cmd.CommandText =
                            @"INSERT Into public.donation (donation_id,title,description,quantity,takeDonation)values(@donation_id,@title,@description,@quantity,@takeDonation)";
                        cmd.Parameters.AddWithValue("donation_id", donation.DonationId);
                        cmd.Parameters.AddWithValue("title", donation.Title);
                        cmd.Parameters.AddWithValue("description", donation.Description);
                        cmd.Parameters.AddWithValue("quantity", donation.Quantity);
                        cmd.Parameters.AddWithValue("takeDonation", donation.TakeDonation);
                        cmd.ExecuteNonQuery();

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

                        cmd.CommandText =
                            @"UPDATE donation SET title = @title, description = @description, quantity = @quantity, takeDonation = @takeDonation  WHERE donation_id = @donation_id";
                        cmd.Parameters.AddWithValue("donation_id", donation.DonationId);
                        cmd.Parameters.AddWithValue("title", donation.Title);
                        cmd.Parameters.AddWithValue("description", donation.Description);
                        cmd.Parameters.AddWithValue("quantity", donation.Quantity);
                        cmd.Parameters.AddWithValue("takeDonation", donation.TakeDonation);
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
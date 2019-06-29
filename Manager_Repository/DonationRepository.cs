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

                        cmd.CommandText = @"DELETE FROM address WHERE address_id=@address_id";
                        cmd.Parameters.AddWithValue("address_id", donation.Address.AddressId);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = @"DELETE FROM donation_affinity WHERE donation_id=@donation_id";
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

                //localiza a doação
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM donation WHERE donation_id=@donation_id";
                cmd.Parameters.AddWithValue("donation_id", id.ToString());
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    donation = new Donation();
                    donation.DonationId = Guid.Parse(reader["donation_id"].ToString());
                    donation.UserId = Guid.Parse(reader["user_id"].ToString());
                    donation.Title = reader["Title"].ToString();
                    donation.Description = reader["Description"].ToString();
                    donation.Quantity = reader["Quantity"].ToString();
                    donation.TakeDonation = (bool) reader["TakeDonation"];
                    donation.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };
                }

                reader.Close();
                cmd.Parameters.Clear();

                //localiza o usuário que realizou a doação               
                cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                cmd.Parameters.AddWithValue("user_id", donation.UserId.ToString());
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    donation.UserId = Guid.Parse(reader["user_id"].ToString());
                    donation.IsApproved = (bool) reader["is_approved"];
                    donation.IsEntity = (bool) reader["is_entity"];
                    donation.IsVoluntary = (bool) reader["is_voluntary"];
                    donation.IsModerator = (bool) reader["is_moderator"];
                    donation.Email = reader["email"].ToString();
                    donation.Password = reader["password"].ToString();
                }

                reader.Close();
                cmd.Parameters.Clear();

                //localiza o endereço da doação
                cmd.CommandText = @"SELECT * FROM address WHERE address_id=@Id";
                cmd.Parameters.AddWithValue("Id", donation.Address.AddressId.ToString());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    donation.Address.CEP = reader["cep"].ToString();
                    donation.Address.Avenue = reader["avenue"].ToString();
                    donation.Address.Number = reader["number"].ToString();
                    donation.Address.Neighborhood = reader["neighborhood"].ToString();
                    donation.Address.City = reader["city"].ToString();
                    donation.Address.State = reader["state"].ToString();
                }

                reader.Close();
                cmd.Parameters.Clear();
            }

            return donation;
        }

        public List<Donation> FindByAffinityId(Guid id)
        {
            List<Donation> donations = new List<Donation>();

            Console.WriteLine("GET ALL BY AFFINITY REPOSITORY");

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                //localiza todos todas as doações de determinada afinidade
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM donation_affinity WHERE affinity_id=@affinity_id";
                cmd.Parameters.AddWithValue("affinity_id", id.ToString());
                var reader = cmd.ExecuteReader();

                List<string> donation_id = new List<string>();
                while (reader.Read())
                {
                    donation_id.Add(reader["donation_id"].ToString());
                }

                //para cada doação localizada, encontra os dados relacionados
                foreach (string don_id in donation_id)
                {
                    reader.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"SELECT * FROM donation WHERE donation_id=@don_id";
                    cmd.Parameters.AddWithValue("Id", don_id);

                    Donation donation = new Donation();

                    while (reader.Read())
                    {
                        donation = new Donation();
                        donation.DonationId = Guid.Parse(reader["donation_id"].ToString());
                        donation.UserId = Guid.Parse(reader["user_id"].ToString());
                        donation.Title = reader["title"].ToString();
                        donation.Quantity = reader["quantity"].ToString();
                        donation.TakeDonation = (bool) reader["takeDonation"];
                        donation.Address = new Address()
                        {
                            AddressId = Guid.Parse(reader["address_id"].ToString())
                        };
                    }

                    reader.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                    cmd.Parameters.AddWithValue("user_id", donation.UserId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        donation.UserId = Guid.Parse(reader["user_id"].ToString());
                        donation.IsApproved = (bool) reader["is_approved"];
                        donation.IsEntity = (bool) reader["is_entity"];
                        donation.IsVoluntary = (bool) reader["is_voluntary"];
                        donation.IsModerator = (bool) reader["is_moderator"];
                        donation.Email = reader["email"].ToString();
                        donation.Password = reader["password"].ToString();
                    }

                    reader.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"SELECT * FROM address WHERE address_id=@Id";
                    cmd.Parameters.AddWithValue("Id", donation.Address.AddressId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        donation.Address.CEP = reader["cep"].ToString();
                        donation.Address.Avenue = reader["avenue"].ToString();
                        donation.Address.Number = reader["number"].ToString();
                        donation.Address.Neighborhood = reader["neighborhood"].ToString();
                        donation.Address.City = reader["city"].ToString();
                        donation.Address.State = reader["state"].ToString();
                    }

                    reader.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText =
                        @"SELECT * FROM affinity AF join donation_affinity da on af.affinity_id = da.affinity_id join donation do on do.donation_id = da.donation_id WHERE do.voluntary_id = @Id";
                    cmd.Parameters.AddWithValue("Id", donation.DonationId.ToString());
                    reader = cmd.ExecuteReader();
                    donation.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        Console.WriteLine("encontrou as afinidades das doações");

                        donation.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["voluntary_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }

                    donations.Add(donation);
                }
            }

            return donations;
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
                    donation.UserId = Guid.Parse(reader["user_id"].ToString());
                    donation.Title = reader["Title"].ToString();
                    donation.Description = reader["Description"].ToString();
                    donation.Quantity = reader["Quantity"].ToString();
                    donation.TakeDonation = (bool) reader["TakeDonation"];
                    donation.Address = new Address()
                    {
                        AddressId = Guid.Parse(reader["address_id"].ToString())
                    };

                    donations.Add(donation);
                }

                //para cada doação localizada, encontra os dados relacionados
                foreach (Donation donation in donations)
                {
                    reader.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                    cmd.Parameters.AddWithValue("user_id", donation.UserId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        donation.UserId = Guid.Parse(reader["user_id"].ToString());
                        donation.IsApproved = (bool) reader["is_approved"];
                        donation.IsEntity = (bool) reader["is_entity"];
                        donation.IsVoluntary = (bool) reader["is_voluntary"];
                        donation.IsModerator = (bool) reader["is_moderator"];
                        donation.Email = reader["email"].ToString();
                        donation.Password = reader["password"].ToString();
                    }

                    reader.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"SELECT * FROM address WHERE address_id=@Id";
                    cmd.Parameters.AddWithValue("Id", donation.Address.AddressId.ToString());
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        donation.Address.CEP = reader["cep"].ToString();
                        donation.Address.Avenue = reader["avenue"].ToString();
                        donation.Address.Number = reader["number"].ToString();
                        donation.Address.Neighborhood = reader["neighborhood"].ToString();
                        donation.Address.City = reader["city"].ToString();
                        donation.Address.State = reader["state"].ToString();
                    }

                    reader.Close();
                    cmd.Parameters.Clear();

                    cmd.CommandText =
                        @"SELECT * FROM affinity AF join donation_affinity da on af.affinity_id = da.affinity_id join donation do on do.donation_id = da.donation_id WHERE do.voluntary_id = @Id";
                    cmd.Parameters.AddWithValue("Id", donation.DonationId.ToString());
                    reader = cmd.ExecuteReader();
                    donation.Affinities = new List<Affinity>();
                    while (reader.Read())
                    {
                        Console.WriteLine("encontrou as afinidades das doações");

                        donation.Affinities.Add(new Affinity()
                        {
                            AffinityId = Guid.Parse(reader["voluntary_id"].ToString()),
                            Name = reader["name"].ToString()
                        });
                    }

                    donations.Add(donation);
                }
            }

            return donations;
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
                        cmd.Parameters.AddWithValue("user_id", donation.UserId);
                        cmd.Parameters.AddWithValue("title", donation.Title);
                        cmd.Parameters.AddWithValue("description", donation.Description);
                        cmd.Parameters.AddWithValue("quantity", donation.Quantity);
                        cmd.Parameters.AddWithValue("takeDonation", donation.TakeDonation);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText =
                            @"INSERT Into public.user (user_id,email,password,is_approved,is_voluntary)values(@user_id,@email,@password,@is_approved,@is_voluntary)";
                        cmd.Parameters.AddWithValue("user_id", donation.UserId);
                        cmd.Parameters.AddWithValue("email", donation.Email);
                        cmd.Parameters.AddWithValue("password", donation.Password);
                        cmd.Parameters.AddWithValue("is_approved", donation.IsApproved);
                        cmd.Parameters.AddWithValue("is_voluntary", donation.IsVoluntary);
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandText =
                            @"INSERT Into address (address_id,cep,avenue,number,neighborhood,city,state)values(@address_id,@cep,@avenue,@number,@neighborhood,@city,@state)";
                        cmd.Parameters.AddWithValue("address_id", donation.Address.AddressId);
                        cmd.Parameters.AddWithValue("cep", donation.Address.CEP);
                        cmd.Parameters.AddWithValue("avenue", donation.Address.Avenue);
                        cmd.Parameters.AddWithValue("number", donation.Address.Number);
                        cmd.Parameters.AddWithValue("neighborhood", donation.Address.Neighborhood);
                        cmd.Parameters.AddWithValue("city", donation.Address.City);
                        cmd.Parameters.AddWithValue("state", donation.Address.State);
                        cmd.ExecuteNonQuery();

                        foreach (var affinity in donation.Affinities)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = @"INSERT Into donation_affinity (donation_id, affinity_id) VALUES (@donation_id, @affinity_id)";
                            cmd.Parameters.AddWithValue("voluntary_id", donation.DonationId);
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

                        cmd.CommandText =
                            @"UPDATE donation SET title = @title, description = @description, quantity = @quantity, takeDonation = @takeDonation  WHERE donation_id = @donation_id";
                        cmd.Parameters.AddWithValue("donation_id", donation.DonationId);
                        cmd.Parameters.AddWithValue("title", donation.Title);
                        cmd.Parameters.AddWithValue("description", donation.Description);
                        cmd.Parameters.AddWithValue("quantity", donation.Quantity);
                        cmd.Parameters.AddWithValue("takeDonation", donation.TakeDonation);
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
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
            throw new NotImplementedException();
        }

        public Voluntary Find(Guid id)
        {
            Console.WriteLine("GET ONE REPOSITORY");
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
                    voluntary.Id = Guid.Parse(reader["Id"].ToString());
                    voluntary.VoluntaryName = reader["VoluntaryName"].ToString();
                    voluntary.VoluntaryEmail = reader["VoluntaryEmail"].ToString();
                    voluntary.VoluntaryPhone = reader["VoluntaryPhone"].ToString();
                    voluntary.VoluntaryPassword = reader["VoluntaryPassword"].ToString();
                    voluntary.VoluntaryCEP = reader["VoluntaryCEP"].ToString();
                    voluntary.VoluntaryAvenue = reader["VoluntaryAvenue"].ToString();
                    voluntary.VoluntaryNumber = reader["VoluntaryNumber"].ToString();
                    voluntary.VoluntaryNeighborhood = reader["VoluntaryNeighborhood"].ToString();
                    voluntary.VoluntaryCity = reader["VoluntaryCity"].ToString();
                    voluntary.VoluntaryState = reader["VoluntaryState"].ToString();
                    voluntary.VoluntaryAffinity = reader["VoluntaryAffinity"].ToString();
                    voluntary.VoluntarySocialNetwork = reader["VoluntarySocialNetwork"].ToString();
                    voluntary.VoluntaryPhotoImageName = reader["VoluntaryPhotoImageName"].ToString();

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
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"INSERT Into voluntary (Id, VoluntaryName, VoluntaryEmail, VoluntaryPhone, VoluntaryPassword, VoluntaryCEP, VoluntaryAvenue, VoluntaryNumber, VoluntaryNeighborhood, VoluntaryCity, VoluntaryState, VoluntaryReferencePoint, VoluntaryAffinity, VoluntarySocialNetwork, VoluntaryPhotoImageName ) values(@Id, @VoluntaryName, @VoluntaryEmail, @VoluntaryPhone, @VoluntaryPassword, @VoluntaryCEP, @VoluntaryAvenue, @VoluntaryNumber, @VoluntaryNeighborhood, @VoluntaryCity, @VoluntaryState, @VoluntaryReferencePoint, @VoluntaryAffinity, @VoluntarySocialNetwork, @VoluntaryPhotoImageName)";
                        cmd.Parameters.AddWithValue("Id", new Guid());
                        cmd.Parameters.AddWithValue("VoluntaryName", voluntary.VoluntaryName); 
                        cmd.Parameters.AddWithValue("VoluntaryEmail", voluntary.VoluntaryEmail); 
                        cmd.Parameters.AddWithValue("VoluntaryPhone", voluntary.VoluntaryPhone); 
                        cmd.Parameters.AddWithValue("VoluntaryPassword", voluntary.VoluntaryPassword); 
                        cmd.Parameters.AddWithValue("VoluntaryCEP", voluntary.VoluntaryCEP); 
                        cmd.Parameters.AddWithValue("VoluntaryAvenue", voluntary.VoluntaryAvenue); 
                        cmd.Parameters.AddWithValue("VoluntaryNumber", voluntary.VoluntaryNumber); 
                        cmd.Parameters.AddWithValue("VoluntaryNeighborhood", voluntary.VoluntaryNeighborhood); 
                        cmd.Parameters.AddWithValue("VoluntaryCity", voluntary.VoluntaryCity); 
                        cmd.Parameters.AddWithValue("VoluntaryState", voluntary.VoluntaryState); 
                        cmd.Parameters.AddWithValue("VoluntaryAffinity", voluntary.VoluntaryAffinity); 
                        cmd.Parameters.AddWithValue("VoluntarySocialNetwork", voluntary.VoluntarySocialNetwork);
                        cmd.Parameters.AddWithValue("VoluntaryPhotoImageName", voluntary.VoluntaryPhotoImageName);
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
            throw new NotImplementedException();
        }
    }
}
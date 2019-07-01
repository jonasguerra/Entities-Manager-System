using System;
using System.Collections.Generic;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;
using Npgsql;

namespace Ftec.WebAPI.Infra.Repository
{
    public class UserRepository: IUserRepository
    {
        
        private string connectionString;
        
        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public Guid Insert(User user)
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
                        cmd.CommandText = @"INSERT Into public.user (user_id,email,password,is_approved,is_voluntary, is_entity, is_moderator)
                                            values(@user_id,@email,@password,@is_approved,@is_voluntary,@is_entity,@is_moderator)";
                        cmd.Parameters.AddWithValue("user_id", user.UserId);
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("password", user.Password); 
                        cmd.Parameters.AddWithValue("is_approved", user.IsApproved);
                        cmd.Parameters.AddWithValue("is_voluntary", user.IsVoluntary);
                        cmd.Parameters.AddWithValue("is_entity", user.IsEntity);
                        cmd.Parameters.AddWithValue("is_moderator", user.IsModerator);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        return user.UserId;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public User Find(Guid id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        User user = new User();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"SELECT * FROM public.user WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", id.ToString());
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            user.UserId = Guid.Parse(reader["user_id"].ToString());
                            user.IsApproved = (bool)reader["is_approved"];
                            user.IsEntity = (bool)reader["is_entity"];
                            user.IsVoluntary = (bool)reader["is_voluntary"];
                            user.IsModerator = (bool)reader["is_moderator"];
                            user.Email = reader["email"].ToString();
                            user.Password = reader["password"].ToString();
                        }
                        return user;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public User Find(string email)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        User user = new User();
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"SELECT * FROM public.user WHERE email=@email";
                        cmd.Parameters.AddWithValue("email", email);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            user.UserId = Guid.Parse(reader["user_id"].ToString());
                            user.IsApproved = (bool)reader["is_approved"];
                            user.IsEntity = (bool)reader["is_entity"];
                            user.IsVoluntary = (bool)reader["is_voluntary"];
                            user.IsModerator = (bool)reader["is_moderator"];
                            user.Email = reader["email"].ToString();
                            user.Password = reader["password"].ToString();
                        }
                        return user;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public List<User> FindAll()
        {
            List<User> users = new List<User>();

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT * FROM public.user";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.UserId = Guid.Parse(reader["user_id"].ToString());
                    user.IsApproved = (bool)reader["is_approved"];
                    user.IsEntity = (bool)reader["is_entity"];
                    user.IsVoluntary = (bool)reader["is_voluntary"];
                    user.IsModerator = (bool)reader["is_moderator"];
                    user.Email = reader["email"].ToString();
                    user.Password = reader["password"].ToString();

                    users.Add(user);
                }
                
                return users;
            }
        }

        public Guid Update(User user)
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
                        cmd.CommandText = @"UPDATE public.user SET email=@email,password=@password,is_approved=@is_approved WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", user.UserId.ToString());
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("password", user.Password); 
                        cmd.Parameters.AddWithValue("is_approved", user.IsApproved);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        return user.UserId;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
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
                        NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = trans;
                        cmd.CommandText = @"DELETE FROM public.user WHERE user_id=@user_id";
                        cmd.Parameters.AddWithValue("user_id", id.ToString());
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
    }
}
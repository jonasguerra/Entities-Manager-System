using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminManagerSystem.Models;
using Ftec.WebAPI.Infra.Repository;
using Manager_Application;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;

namespace Manager_API.Controllers
{
    public class UserController : ApiController
    {
        private UserAplication userApplication;
        private IUserRepository userRepository;

        public UserController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            userRepository = new UserRepository(connectionString);
            userApplication = new UserAplication(userRepository);
        }
        
        
        //get by email
        public HttpResponseMessage Get(string email)
        {
            try
            {
                UserDTO userDTO = Find(email);
                if (userDTO == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Usuário não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, userDTO);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                UserDTO userDto = Find(id);
                if (userDto== null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Usuário não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, userDto);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Get()
        {
            List<UserDTO> usersDTO = new List<UserDTO>();
            try
            {
                usersDTO = userApplication.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, usersDTO);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                UserDTO userDTO = Find(id);
                if (userDTO == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Usuário não encontrado");
                }
                else
                {
                    bool removed = Remove(id);
                    if (removed)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, id);
                    }
                }
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Erro ao excluir voluntário");
        }
        
        public HttpResponseMessage Post([FromBody]User user)
        {
            try
            {
                Guid id = Insert(user);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (ApplicationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        private Guid Insert(User user)
        {
            UserDTO userDTO = new UserDTO ()
            {
                UserId = user.UserId,
                Email = user.Email,
                IsApproved = user.IsApproved,
                IsModerator = user.IsModerator,
                IsEntity = user.IsEntity,
                IsVoluntary = user.IsVoluntary,
                Password = user.Password,
            };
            return userApplication.Insert(userDTO);
        }
        
        //find by email
        private UserDTO Find(string email)
        {
            return userApplication.GetByEmail(email);
        }
        
        private UserDTO Find(Guid id)
        {
            return userApplication.Get(id);
        }
        
        private bool Remove(Guid id)
        {
            return userApplication.Delete(id);
        }
    }
}
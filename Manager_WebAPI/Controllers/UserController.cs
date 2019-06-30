using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        
        private UserDTO Find(string email)
        {
            return userApplication.GetByEmail(email);
        }
        
    }
}
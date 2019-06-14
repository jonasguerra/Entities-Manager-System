using System;
using System.Collections.Generic;
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
    public class AffinityController : ApiController
    {
        private IAffinityRepository affinityRepository;
        private AffinityAplication affinityApplication;

        public AffinityController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            //injetando a dependencia do repositorio na aplicação
            affinityRepository = new AffinityRepository(connectionString);
            affinityApplication = new AffinityAplication(affinityRepository);
        }

        // GET api/values
        public HttpResponseMessage Get()
        {
            List<AffinityDTO> affinitiesDTO = new List<AffinityDTO>();
            try
            {
                affinitiesDTO = affinityApplication.GetAll();

                //Este metodo retorna uma listagem de voluntarios
                return Request.CreateResponse(HttpStatusCode.OK, affinitiesDTO);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        // GET api/values/5
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                AffinityDTO affinitiesDTO = Find(id);
                //Neste local faria uma busca na base de dados 
                // Se encontrar o voluntario retorno o voluntario, caso contrário retorna código de nao encontrado
                if (affinitiesDTO == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Afinidade não encontrada");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, affinitiesDTO);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        
        private AffinityDTO Find(Guid id)
        {
            return affinityApplication.Get(id);
        }
        
    }
}
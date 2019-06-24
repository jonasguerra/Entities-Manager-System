using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EntitiesManagerSystem.Models;
using Ftec.WebAPI.Infra.Repository;
using Manager_Application;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;

namespace Manager_API.Controllers
{
    public class EntityController : ApiController
    {
        private IEntityRepository entityRepository;
        private EntityApplication entityApplication;



        public EntityController()
        {
            string connectionString = string.Empty;
            //injetando a dependencia do repositorio na aplicação
            entityRepository = new EntityRepository(connectionString);
            entityApplication = new EntityApplication(entityRepository);
        }

        public HttpResponseMessage Get()
        {
            List<Entity> entities = new List<Entity>();
            try
            {
                Entity entity = new Entity()
                {
                    EntityName = "",
                    EntityCity = "",
                    EntityEmail = "",
                    EntityNumber = "",
                    EntityAvenue = "",
                    Id = Guid.NewGuid()
                    //falta algumas propriedades para preencher
                };
                entities.Add(entity);
                return Request.CreateResponse(HttpStatusCode.OK,entities);
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
                EntityDTO entityDto = Find(id);
                if (entityDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Voluntário não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entityDto);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        private EntityDTO Find(Guid id)
        {
            return entityApplication.Get(id);
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

    }
}

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
            List<EntityDTO> entitiesDTO = new List<EntityDTO>();
            try
            {
                entitiesDTO = entityApplication.GetAll();
                //Este metodo retorna uma listagem de entidades
                return Request.CreateResponse(HttpStatusCode.OK, entitiesDTO);    
                    
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
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Entidade não encontrada");
                }else{
                    return Request.CreateResponse(HttpStatusCode.OK, entityDto);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
            
        }



        public HttpResponseMessage Post([FromBody] Entity entity)
        {
            try
            {
                //Neste local faria a inclusao da entidade no repositorio
                //Antes de fazer a inclusão a entidade seria consistido
                //Se a entidade for inserida é de responsabilidade da API retornar o código da entidade gerada no processo de inclusão

                Guid id = Insert(entity);
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




        public HttpResponseMessage Put(Guid id, [FromBody] Entity entity)
        {
            try
            {
               
                EntityDTO entityDto= Find(id);
                if (entityDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Entidade não encontrada");
                }
                else
                {
                    Alter(entity);
                    return Request.CreateResponse(HttpStatusCode.OK, id);
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
        }
        

        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
               
               EntityDTO entityDto= Find(id);
                if (entityDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Entidade não encontrada");
                }
                else
                {
                    Remove(id);
                    return Request.CreateResponse(HttpStatusCode.OK, id);
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
        }
        
        
        private void Alter(Entity entity)
        {
            EntityDTO entityDto = new EntityDTO()
            {
                Id = entity.Id,
                EntityName = entity.EntityName,
                EntityAffinity = entity.EntityAffinity,
                EntityAvenue = entity.EntityAvenue,
                EntityCity = entity.EntityCity,
                EntityDescription = entity.EntityDescription,
                EntityEmail = entity.EntityEmail,
                EntityNeighborhood = entity.EntityNeighborhood,
                EntityInitials =  entity.EntityInitials,
                EntityNumber = entity.EntityNumber,
                EntityPhone = entity.EntityPhone,
                EntityPassword = entity.EntityPassword,
                EntityState = entity.EntityState,
                EntityCreationDate = entity.EntityCreationDate,
                EntityConfirmPassword = entity.EntityConfirmPassword,
                EntityReferencePoint = entity.EntityReferencePoint,
                EntityWebSite = entity.EntityWebSite,
                EntityResponsableName = entity.EntityResponsableName,
                EntitySocialNetwork = entity.EntitySocialNetwork,
                EntityCEP = entity.EntityCEP,
                EntityPhotoImageName = entity.EntityPhotoImageName

            };
            entityApplication.Update(entityDto);
        }

        
        private Guid Insert(Entity entity)
        {
            //return Guid.NewGuid();
            //executa o mapeamento
            EntityDTO entityDTO = new EntityDTO()
            {
                Id = entity.Id,
                EntityName = entity.EntityName,
                EntityAffinity = entity.EntityAffinity,
                EntityAvenue = entity.EntityAvenue,
                EntityCity = entity.EntityCity,
                EntityDescription = entity.EntityDescription,
                EntityEmail = entity.EntityEmail,
                EntityNeighborhood = entity.EntityNeighborhood,
                EntityInitials =  entity.EntityInitials,
                EntityNumber = entity.EntityNumber,
                EntityPhone = entity.EntityPhone,
                EntityPassword = entity.EntityPassword,
                EntityState = entity.EntityState,
                EntityCreationDate = entity.EntityCreationDate,
                EntityConfirmPassword = entity.EntityConfirmPassword,
                EntityReferencePoint = entity.EntityReferencePoint,
                EntityWebSite = entity.EntityWebSite,
                EntityResponsableName = entity.EntityResponsableName,
                EntitySocialNetwork = entity.EntitySocialNetwork,
                EntityCEP = entity.EntityCEP,
                EntityPhotoImageName = entity.EntityPhotoImageName
            };
           
            return entityApplication.Insert(entityDTO);
        }
        
        
       

        
        private EntityDTO Find(Guid id)
        {
            return entityApplication.Get(id);
        }
        private void Remove(Guid id)
        {
            entityApplication.Delete(id);
        }
        
    }
}
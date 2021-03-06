﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ftec.WebAPI.Infra.Repository;
using Manager_API.Models.Voluntary;
using Manager_Application;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;

namespace Manager_API.Controllers
{
    public class VoluntaryController : ApiController
    {
        
        private IVoluntaryRepository voluntaryRepository;
        private VoluntaryApplication voluntaryApplication;

        public VoluntaryController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            //injetando a dependencia do repositorio na aplicação
            voluntaryRepository = new VoluntaryRepository(connectionString);
            voluntaryApplication = new VoluntaryApplication(voluntaryRepository);
        }
        
        
        // GET api/values
        public HttpResponseMessage Get()
        {
            List<VoluntaryDTO> voluntariesDTO = new List<VoluntaryDTO>();
            try
            {
                voluntariesDTO = voluntaryApplication.GetAll();
       
                //Este metodo retorna uma listagem de voluntarios
                return Request.CreateResponse(HttpStatusCode.OK, voluntariesDTO);
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
                VoluntaryDTO voluntariesDTO = Find(id);
                //Neste local faria uma busca na base de dados 
                // Se encontrar o voluntario retorno o voluntario, caso contrário retorna código de nao encontrado
                if (voluntariesDTO == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Voluntário não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, voluntariesDTO);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Voluntary voluntary)
        {
            Console.WriteLine("POST CONTROLLER");
            try
            {
                //Neste local faria a inclusao do voluntario no repositorio
                //Antes de fazer a inclusão o voluntario seria consistido
                //Se o voluntario for inserido é de responsabilidade da API retornar o código do voluntario gerado no processo de inclusão

                Guid id = Insert(voluntary);
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

        // PUT api/values/5
        public HttpResponseMessage Put(Guid id, [FromBody]Voluntary voluntary)
        {
            Console.WriteLine("PUT CONTROLLER");
            try
            {
                //Neste local faria a alteracao do voluntario no repositorio
                //Antes de fazer a alteracao, o sistema deve verificar se existe o voluntario
                //Se existir aplica as mudanças
                VoluntaryDTO voluntaryDto = Find(id);
                if (voluntaryDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Voluntario não encontrado");
                }
                else
                {
                    Alter(voluntary);
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

        // DELETE api/values/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                VoluntaryDTO voluntaryDto = Find(id);
                if (voluntaryDto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Voluntario não encontrado");
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

        private Guid Insert(Voluntary voluntary)
        {
            Console.WriteLine("POST METHOD CONTROLLER");
            //return Guid.NewGuid();
            //executa o mapeamento
            
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            foreach(var affinity in voluntary.Affinities)
            {
                AffinityDTO affinityDTO = new AffinityDTO()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                affinities.Add(affinityDTO);
            }
            
            VoluntaryDTO voluntaryDTO = new VoluntaryDTO ()
            {
                VoluntaryId = voluntary.VoluntaryId,
                UserId = voluntary.UserId,
                IsApproved = voluntary.IsApproved,
                IsVoluntary = voluntary.IsVoluntary,
                Name = voluntary.Name,
                Email = voluntary.Email,
                Phone = voluntary.Phone,
                Password = voluntary.Password,
                SocialNetwork = voluntary.SocialNetwork, 
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    CEP = voluntary.Address.CEP,
                    Avenue = voluntary.Address.Avenue,
                    Number = voluntary.Address.Number,
                    Neighborhood = voluntary.Address.Neighborhood,
                    City = voluntary.Address.City,
                    State = voluntary.Address.State
                }
            };
           
            return voluntaryApplication.Insert(voluntaryDTO);
        }

        private void Alter(Voluntary voluntary)
        {
            
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            foreach(var affinity in voluntary.Affinities)
            {
                AffinityDTO affinityDTO = new AffinityDTO()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                affinities.Add(affinityDTO);
            }
            
            VoluntaryDTO voluntaryDTO = new VoluntaryDTO ()
            {
                VoluntaryId = voluntary.VoluntaryId,
                UserId = voluntary.UserId,
                IsApproved = voluntary.IsApproved,
                IsVoluntary = voluntary.IsVoluntary,
                Name = voluntary.Name,
                Email = voluntary.Email,
                Phone = voluntary.Phone,
                Password = voluntary.Password,
                SocialNetwork = voluntary.SocialNetwork, 
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    CEP = voluntary.Address.CEP,
                    Avenue = voluntary.Address.Avenue,
                    Number = voluntary.Address.Number,
                    Neighborhood = voluntary.Address.Neighborhood,
                    City = voluntary.Address.City,
                    State = voluntary.Address.State
                }
            };
            
            voluntaryApplication.Update(voluntaryDTO);
        }

        private bool Remove(Guid id)
        {
            return voluntaryApplication.Delete(id);
        }

        private VoluntaryDTO Find(Guid id)
        {
            return voluntaryApplication.Get(id);
        }
    }
}

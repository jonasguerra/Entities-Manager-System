using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Manager_API.Models.Voluntary;

namespace Manager_API.Controllers
{
    public class VoluntaryController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            Console.WriteLine("GET METHOD");
            List<Voluntary> voluntaries = new List<Voluntary>();
            try
            {
                Voluntary voluntary = new Voluntary()
                {
                    VoluntaryName = "Nome",
                    VoluntaryEmail = "email@email.com",
                    Id = Guid.NewGuid()
                };

                voluntaries.Add(voluntary);
                voluntaries.Add(voluntary);
                voluntaries.Add(voluntary);
                //Este metodo retorna uma listagem de voluntarios
                return Request.CreateResponse(HttpStatusCode.OK, voluntaries);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/values/5
        public HttpResponseMessage Get(Guid id)
        {
            Console.WriteLine("GET ONE METHOD");
            try
            { 
                Voluntary voluntary = new Voluntary()
                {
                    VoluntaryName = "Nome",
                    VoluntaryEmail = "email@email.com",
                    Id = Guid.NewGuid()
                };
                //Neste local faria uma busca na base de dados 
                // Se encontrar o voluntario retorno o voluntario, caso contrário retorna código de nao encontrado
                if (voluntary == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Voluntário não encontrado");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, voluntary);
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
            Console.WriteLine("POST METHOD");
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
            Console.WriteLine("PUT METHOD");
            try
            {
                //Neste local faria a alteracao do voluntario no repositorio
                //Antes de fazer a alteracao, o sistema deve verificar se existe o voluntario
                //Se existir aplica as mudanças
                Voluntary cli = Find(id);
                if (cli == null)
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
            Console.WriteLine("DELETE METHOD");
            try
            {
                //Neste local faria a exclusão do voluntario no repositorio
                Voluntary cli = Find(id);
                if (cli == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Voluntario não encontrado");
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

        private Guid Insert(Voluntary voluntary)
        {
            return Guid.NewGuid();
        }

        private void Alter(Voluntary voluntary)
        {
            
        }

        private void Remove(Guid id)
        {

        }

        private Voluntary Find(Guid id)
        {
            return  new Voluntary()
            {
                VoluntaryName = "Nome",
                VoluntaryEmail = "email@email.com",
                Id = Guid.NewGuid()
            };
        }
    }
}

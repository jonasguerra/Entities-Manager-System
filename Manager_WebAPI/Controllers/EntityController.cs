using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Ftec.WebAPI.Infra.Repository;
using Manager_Application;
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

        
    }
}

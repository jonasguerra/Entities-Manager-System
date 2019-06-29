using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ftec.WebAPI.Infra.Repository;
using Manager_Application;
using Manager_Application.DTO;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

namespace Manager_API.Controllers
{
    public class DonationController : ApiController
    {
        private IDonationRepository DonationRepository;
        private DonationApplication DonationApplication;


        public DonationController()
        {
            //string de conexão com o banco
            string connectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
            //injetando a dependencia do repositorio na aplicação
            DonationRepository = new DonationRepository(connectionString);
            DonationApplication = new DonationApplication(DonationRepository);
        }


        // POST
        public HttpResponseMessage POST([FromBody] Donation donation)
        {
            Console.WriteLine("POST DONATION CONTROLLER");

            try
            {
                Guid id = Insert(donation);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (ApplicationException exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        private Guid Insert(Donation donation)
        {
            Console.WriteLine("POST DONATION INSERT CONTROLLER");

            List<AffinityDTO> affinities = new List<AffinityDTO>();

            foreach (var affinity in donation.Affinities)
            {
                AffinityDTO affinityDTO = new AffinityDTO()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                affinities.Add(affinityDTO);
            }

            DonationDTO donationDto = new DonationDTO()
            {
                DonationId = donation.DonationId,
                UserId = donation.UserId,
                Title = donation.Title,
                Description = donation.Description,
                Quantity = donation.Quantity,
                TakeDonation = donation.TakeDonation,
                Affinities = affinities,

                Address = new AddressDTO()
                {
                    AddressId = donation.Address.AddressId,
                    CEP = donation.Address.CEP,
                    Avenue = donation.Address.Avenue,
                    Number = donation.Address.Number,
                    Neighborhood = donation.Address.Neighborhood,
                    City = donation.Address.City,
                    State = donation.Address.State
                }
            };

            return DonationApplication.Insert(donationDto);
        }
    }
}
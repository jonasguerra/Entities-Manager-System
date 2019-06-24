using System;
using System.Collections.Generic;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;
using Manager_Domain.Entities;

namespace Manager_Application
{
    public class VoluntaryApplication
    {
        IVoluntaryRepository voluntaryRepository;

        public VoluntaryApplication(IVoluntaryRepository voluntaryRepository)
        {
            this.voluntaryRepository = voluntaryRepository;
        }

        public Guid Insert(VoluntaryDTO voluntaryDto)
        {
            Console.WriteLine("POST APPLICATION");

            var voluntary = VoluntaryAdapter.ToDomain(voluntaryDto);

            return voluntaryRepository.Insert(voluntary);
        }

        public Guid Update(VoluntaryDTO voluntaryDto)
        {
            Console.WriteLine("PUT APPLICATION");

            var voluntary = VoluntaryAdapter.ToDomain(voluntaryDto);

            return voluntaryRepository.Update(voluntary);
        }

        public bool Delete(Guid id)
        {
            Console.WriteLine("DELETE APPLICATION");
            return voluntaryRepository.Delete(id);
        }

        public VoluntaryDTO Get(Guid id)
        {
            Console.WriteLine("GET ONE APPLICATION");

            var voluntary = voluntaryRepository.Find(id);

            return VoluntaryAdapter.ToDTO(voluntary);
        }

        public List<VoluntaryDTO> GetAll()
        {
            Console.WriteLine("GET ALL APPLICATION");

            List<VoluntaryDTO> voluntarysDto = new List<VoluntaryDTO>();
            var voluntarys = voluntaryRepository.FindAll();


            foreach (Voluntary cli in voluntarys)
            {
                voluntarysDto.Add(VoluntaryAdapter.ToDTO(cli));
            }

            return voluntarysDto;
        }
    }
}
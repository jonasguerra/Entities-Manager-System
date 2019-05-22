using System;
using System.Collections.Generic;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

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
            
            Console.WriteLine("POST METHOD 3");
            
            var voluntary = VoluntaryAdapter.ToDomain(voluntaryDto);

            return voluntaryRepository.Insert(voluntary);
        }

        public Guid Update(VoluntaryDTO voluntaryDto)
        {
            var voluntary = VoluntaryAdapter.ToDomain(voluntaryDto);

            return voluntaryRepository.Update(voluntary);
        }

        public bool Delete(Guid id)
        {
            return voluntaryRepository.Delete(id);
        }

        public VoluntaryDTO Get(Guid id)
        {
            var voluntary = voluntaryRepository.Find(id);

            return VoluntaryAdapter.ToDTO(voluntary);
        }

        public List<VoluntaryDTO> GetAll()
        {
            List<VoluntaryDTO> voluntarysDto = new List<VoluntaryDTO>();
            var voluntarys = voluntaryRepository.FindAll();


            foreach(Voluntary cli in voluntarys)
            {
                voluntarysDto.Add(VoluntaryAdapter.ToDTO(cli));
            }

            return voluntarysDto;
        }
    }
}
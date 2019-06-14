using System;
using System.Collections.Generic;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;

namespace Manager_Application
{
    public class AffinityAplication
    {
        IAffinityRepository affinityRepository;

        public AffinityAplication(IAffinityRepository affinityRepository)
        {
            this.affinityRepository = affinityRepository;
        }

        public AffinityDTO Get(Guid id)
        {
            var affinity = affinityRepository.Find(id);

            return AffinityAdapter.ToDTO(affinity);
        }

        public List<AffinityDTO> GetAll()
        {
            var affinities = affinityRepository.FindAll();

            List<AffinityDTO> affinitiesDto = AffinityAdapter.ListToDTO(affinities);
            

            return affinitiesDto;
        }
    }
}
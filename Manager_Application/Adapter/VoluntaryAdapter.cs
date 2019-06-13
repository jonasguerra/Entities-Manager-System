using System;
using System.Collections.Generic;
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public static class VoluntaryAdapter
    {
        public static VoluntaryDTO ToDTO(Voluntary voluntary)
        {
            Console.WriteLine("ADAPTER TO DTO");

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
            
            return new VoluntaryDTO()
            {
                VoluntaryId = voluntary.VoluntaryId,
                UserId = voluntary.UserId,
                IsApproved = voluntary.IsApproved,
                IsEntity = voluntary.IsEntity,
                Name = voluntary.Name,
                Email = voluntary.Email,
                Password = voluntary.Password,
                Phone = voluntary.Phone,
                SocialNetwork = voluntary.SocialNetwork,
                PhotoImageName = voluntary.PhotoImageName,
                Affinities = affinities,
                Address = new AddressDTO()
                {
                    AddressId = voluntary.Address.AddressId,
                    CEP = voluntary.Address.CEP,
                    Avenue = voluntary.Address.Avenue,
                    Number = voluntary.Address.Number,
                    Neighborhood = voluntary.Address.Neighborhood,
                    City = voluntary.Address.City,
                    State = voluntary.Address.State
                }
            };
        }

        public static Voluntary ToDomain(VoluntaryDTO voluntary)
        {
            Console.WriteLine("ADAPTER TO DOMAIN");
            
            List<Affinity> affinities = new List<Affinity>();
            foreach(var affinityDTO in voluntary.Affinities)
            {
                AffinityDTO affinity = new AffinityDTO()
                {
                    AffinityId = affinityDTO.AffinityId,
                    Name = affinityDTO.Name
                };
                
                affinities.Add(affinityDTO);
            }
            
            return new Voluntary()
            {
                VoluntaryId = voluntary.VoluntaryId,
                UserId = voluntary.UserId,
                IsApproved = voluntary.IsApproved,
                IsEntity = voluntary.IsEntity,
                Name = voluntary.Name,
                Email = voluntary.Email,
                Phone = voluntary.Phone,
                Password = voluntary.Password,
                SocialNetwork = voluntary.SocialNetwork, 
                PhotoImageName = voluntary.PhotoImageName,
                Address = new Address()
                {
                    AddressId = voluntary.Address.AddressId,
                    CEP = voluntary.Address.CEP,
                    Avenue = voluntary.Address.Avenue,
                    Number = voluntary.Address.Number,
                    Neighborhood = voluntary.Address.Neighborhood,
                    City = voluntary.Address.City,
                    State = voluntary.Address.State
                }
            };
        }
    }
}
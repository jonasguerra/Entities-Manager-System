using System;
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public static class VoluntaryAdapter
    {
        public static VoluntaryDTO ToDTO(Voluntary voluntary)
        {
            Console.WriteLine("ADAPTER TO DTO");

            return new VoluntaryDTO()
            {
                VoluntaryId = voluntary.VoluntaryId,
                IsApproved = voluntary.IsApproved,
                Name = voluntary.Name,
                Email = voluntary.Email,
                Password = voluntary.Password,
                Phone = voluntary.Phone,
                Affinity = voluntary.Affinity,
                SocialNetwork = voluntary.SocialNetwork,
                PhotoImageName = voluntary.PhotoImageName,
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
        }

        public static Voluntary ToDomain(VoluntaryDTO voluntary)
        {
            Console.WriteLine("ADAPTER TO DOMAIN");
            
            return new Voluntary()
            {
                VoluntaryId = voluntary.VoluntaryId,
                IsApproved = voluntary.IsApproved,
                Name = voluntary.Name,
                Email = voluntary.Email,
                Phone = voluntary.Phone,
                Password = voluntary.Password,
                Affinity = voluntary.Affinity,
                SocialNetwork = voluntary.SocialNetwork, 
                PhotoImageName = voluntary.PhotoImageName,
                Address = new Address()
                {
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
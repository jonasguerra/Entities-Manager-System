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
                Id = voluntary.Id,
                VoluntaryName = voluntary.VoluntaryName,
                VoluntaryEmail = voluntary.VoluntaryEmail,
                VoluntaryPhone = voluntary.VoluntaryPhone,
                VoluntaryPassword = voluntary.VoluntaryPassword,
                VoluntaryConfirmPassword = voluntary.VoluntaryConfirmPassword,
                VoluntaryCEP = voluntary.VoluntaryCEP,
                VoluntaryAvenue = voluntary.VoluntaryAvenue,
                VoluntaryNumber = voluntary.VoluntaryNumber,
                VoluntaryNeighborhood = voluntary.VoluntaryNeighborhood,
                VoluntaryCity = voluntary.VoluntaryCity,
                VoluntaryState = voluntary.VoluntaryState,
                VoluntaryReferencePoint = voluntary.VoluntaryReferencePoint,
                VoluntaryAffinity = voluntary.VoluntaryAffinity,
                VoluntarySocialNetwork = voluntary.VoluntarySocialNetwork, 
                VoluntaryPhotoImageName = voluntary.VoluntaryPhotoImageName
             
            };
        }

        public static Voluntary ToDomain(VoluntaryDTO voluntary)
        {
            Console.WriteLine("ADAPTER TO DOMAIN");
            
            return new Voluntary()
            {
                Id = voluntary.Id,
                VoluntaryName = voluntary.VoluntaryName,
                VoluntaryEmail = voluntary.VoluntaryEmail,
                VoluntaryPhone = voluntary.VoluntaryPhone,
                VoluntaryPassword = voluntary.VoluntaryPassword,
                VoluntaryConfirmPassword = voluntary.VoluntaryConfirmPassword,
                VoluntaryCEP = voluntary.VoluntaryCEP,
                VoluntaryAvenue = voluntary.VoluntaryAvenue,
                VoluntaryNumber = voluntary.VoluntaryNumber,
                VoluntaryNeighborhood = voluntary.VoluntaryNeighborhood,
                VoluntaryCity = voluntary.VoluntaryCity,
                VoluntaryState = voluntary.VoluntaryState,
                VoluntaryReferencePoint = voluntary.VoluntaryReferencePoint,
                VoluntaryAffinity = voluntary.VoluntaryAffinity,
                VoluntarySocialNetwork = voluntary.VoluntarySocialNetwork, 
                VoluntaryPhotoImageName = voluntary.VoluntaryPhotoImageName
            };
        }
    }
}
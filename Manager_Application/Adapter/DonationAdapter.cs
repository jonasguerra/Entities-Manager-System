using System.Security.Policy;
using Manager_Domain.Entities;
using Manager_Application.DTO;

namespace Manager_Application.Adapter
{
    public static class DonationAdapter
    {
        public static DonationDTO ToDTO(Donation donation)
        {
            return new DonationDTO()
            {
                DonationId = donation.DonationId,
                UserId = donation.UserId,
                Title = donation.Title,
                Description = donation.Description,
                Quantity = donation.Quantity,
                TakeDonation = donation.TakeDonation,

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
        }

        public static Donation ToDomain(DonationDTO donationDTO)
        {
            return new Donation()
            {
                DonationId = donationDTO.DonationId,
                UserId = donationDTO.UserId,
                Title = donationDTO.Title,
                Description = donationDTO.Description,
                Quantity = donationDTO.Quantity,
                TakeDonation = donationDTO.TakeDonation,

                Address = new Address()
                {
                    AddressId = donationDTO.Address.AddressId,
                    CEP = donationDTO.Address.CEP,
                    Avenue = donationDTO.Address.Avenue,
                    Number = donationDTO.Address.Number,
                    Neighborhood = donationDTO.Address.Neighborhood,
                    City = donationDTO.Address.City,
                    State = donationDTO.Address.State
                }
            };
        }
    }
}
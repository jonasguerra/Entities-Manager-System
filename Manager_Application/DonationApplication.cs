using System;
using System.Runtime.InteropServices;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Interfaces;

namespace Manager_Application
{
    public class DonationApplication
    {
        private IDonationRepository donationRepository;

        public DonationApplication(IDonationRepository donationRepository)
        {
            this.donationRepository = donationRepository;
        }

        public Guid Insert(DonationDTO donationDto)
        {
            Console.WriteLine("INSERT APPLICATION DONATION");

            var donation = DonationAdapter.ToDomain(donationDto);

            return donationRepository.Insert(donation);
        }
    }
}
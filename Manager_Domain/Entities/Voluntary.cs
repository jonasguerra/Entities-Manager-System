using System;

namespace Manager_Domain.Entities
{
    public class Voluntary
    {
        public Guid Id { get; set; }
        public string VoluntaryName { get; set; }
        public string VoluntaryEmail { get; set; }
        public string VoluntaryPhone { get; set; }
        public string VoluntaryPassword { get; set; }
        public string VoluntaryCEP { get; set; }
        public string VoluntaryAvenue { get; set; }
        public string VoluntaryNumber { get; set; }
        public string VoluntaryNeighborhood { get; set; }
        public string VoluntaryCity { get; set; }
        public string VoluntaryState { get; set; }
        public string VoluntaryAffinity { get; set; }
        public string VoluntarySocialNetwork { get; set; }
        public string VoluntaryPhotoImageName { get; set; }
    }
}
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public class AffinityAdapter
    {
        public static AffinityDTO ToDTO(Affinity affinity)
        {
            return new AffinityDTO()
            {
                AffinityId = affinity.AffinityId,
                Name = affinity.Name
            };
                
        }

        public static Affinity ToDTO(AffinityDTO affinity)
        {
            return new Affinity()
            {
                AffinityId = affinity.AffinityId,
                Name = affinity.Name
            };
        }
    }
}
using System.Collections.Generic;
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

        public static Affinity ToDomain(AffinityDTO affinity)
        {
            return new Affinity()
            {
                AffinityId = affinity.AffinityId,
                Name = affinity.Name
            };
        }
        
        public static List<AffinityDTO> ListToDTO(List<Affinity> list_affinity)
        {
            List<AffinityDTO> affinities = new List<AffinityDTO>();
            
            foreach(var affinity in list_affinity)
            {
                AffinityDTO affinityDTO = new AffinityDTO()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                
                affinities.Add(affinityDTO);
            }

            return affinities;
        }

        public static List<Affinity> ListToDomain(List<AffinityDTO> list_affinity)
        {
            
            List<Affinity> affinities = new List<Affinity>();
            
            foreach(var affinity in list_affinity)
            {
                Affinity affinityDTO = new Affinity()
                {
                    AffinityId = affinity.AffinityId,
                    Name = affinity.Name
                };
                
                affinities.Add(affinityDTO);
            }

            return affinities;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace AdminManagerSystem.Models
{
    public class Affinity
    {
        public Guid AffinityId { get; set; }
        public string Name { get; set; }
        public Affinity()
        {
        }
    }
}
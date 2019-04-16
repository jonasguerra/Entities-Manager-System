using System;
using System.ComponentModel.DataAnnotations;

namespace AdminManagerSystem.Models
{
    public class Affinity
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string Name { get; set; }

        public Affinity()
        {
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntitiesManagerSystem.Custom_Atribute;

namespace EntitiesManagerSystem.Models.Voluntary
{
    public class Donations
    {
        public Guid Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string Title { get; set; }

        [Display(Name = "Afinidade")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public string Affinity { get; set; }

        [Display(Name = "Descrição da doação")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(100000, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caractéres")]
        public string Description { get; set; }

        [Display(Name = "Quantidade de itens")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [Range(1, int.MaxValue, ErrorMessage = "Insira uma inteiro válido")]
        public string Quantity { get; set; }
        
        public bool takeDonation { get; set; }
        
        public Address Address { get; set; }    
        
        public List<Affinity> Affinities { get; set; }

        public Donations()
        {
            
        }
    }
}
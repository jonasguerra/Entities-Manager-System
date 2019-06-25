using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        
        [Display(Name = "CEP")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "CEP deve possuir 8 caracteres")]
        public string CEP { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Minimo 8 caracteres")]
        public string Avenue { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Minimo {2} caracteres")]
        public string Number { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Minimo {2} caracteres")]
        public string Neighborhood { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo {2} caracteres")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Minimo {2} caracteres")]
        public string State { get; set; }
    }
}
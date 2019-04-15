using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string Title { get; set; }
        
        
        [Display(Name = "Descrição do evento")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(100000, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caractéres")]
        public string Description { get; set; }
        
//        public string Affinity { get; set; }
       
        
//        public string Date { get; set; }
            
        [Display(Name = "CEP")]
        [RegularExpression("^\\d{5}[-]\\d{3}$", ErrorMessage = "Insira um {0} válido")]
        public string Cep { get; set; }
        
        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caractéres")]
        public string Address { get; set; }
        
        [Display(Name = "Número")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Number { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Neighborhood{ get; set; }
        
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string City{ get; set; }
        
//        public string State { get; set; }

        [Display(Name = "Ponto de referência")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string ReferencePoint{ get; set; }
        
        
        public Event()
        {
            
        }
    }
}
using System;
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
        
        
        [Display(Name = "CEP")]
        [RegularExpression("^\\d{5}[-]\\d{3}$", ErrorMessage = "Insira um {0} válido")]
        public string Cep { get; set; }


        [Display(Name = "Endereço")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caractéres")]
        [RequiredIf("takeDonation", true, "Endereço deve ser informado")] 
        public string Address { get; set; }


        [Display(Name = "Número")]
        [Range(1, int.MaxValue, ErrorMessage = "Insira uma inteiro válido")]
        [RequiredIf("takeDonation", true, "Número deve ser informado")]
        public string Number { get; set; }


        [Display(Name = "Bairro")] 
        [RequiredIf("takeDonation", true, "Bairro deve ser informado")] 
        public string Neighborhood { get; set; }


        [Display(Name = "Cidade")] 
        [RequiredIf("takeDonation", true, "Cidade deve ser informada")] 
        public string City { get; set; }


        [Display(Name = "UF")] 
        [RequiredIf("takeDonation", true, "UF deve ser informado")] 
        public string State { get; set; }


        [Display(Name = "Ponto de referência")]
        public string ReferencePoint { get; set; }


        public Donations()
        {
            
        }
    }
}
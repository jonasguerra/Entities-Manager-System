using System;
using System.ComponentModel.DataAnnotations;
using Manager_API.Models;

namespace EntitiesManagerSystem.Models
{
    public class Entity : User
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Nome da Entidade")]
        [Required(ErrorMessage = "O {0} deve ser informado.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityName { get; set; }

        [Display(Name = "Responsavel pela Entidade")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityResponsableName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityEmail { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Apenas numeros devem ser informados")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityPhone { get; set; }

      
        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public string EntityInitials { get; set; }
        
        [Display(Name = "Data de Criação")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public DateTime EntityCreationDate { get; set; }
        
        [Display(Name = "Redes Sociais")]
        public string EntitySocialNetwork { get; set; }
        
        [Display(Name = "Site")]
        public string EntityWebSite { get; set; }
        
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public string EntityDescription { get; set; }
       
        public Entity()
        {
          IsEntity = true;
        }
    }
}
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

        [Required]
        [Display(Name = "Senha")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "As senhas devem ter pelo menos oito caracteres e conter pelo menos três das seguintes opções: maiúscula (A a Z), minúscula (a-z), número (0 a 9) e caractere especial (por exemplo, @ # $% ^ & *)")]
        [DataType(DataType.Password)]
        public string EntityPassword { get; set; }
        
        [Display(Name = "Confirme a Senha")]
        [Compare("EntityPassword", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string EntityConfirmPassword { get; set; }

       
        [Display(Name = "Ponto de Referencia")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityReferencePoint { get; set; }
        
        [Display(Name = "Afinidade")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public string EntityAffinity { get; set; }
        
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
        
        [Display(Name = "Imagem da Entidade")]
        public string EntityPhotoImageName { get; set; }

        public Entity()
        {
          //  IsEntity = true;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

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

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A {0} deve ser informada")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityPassword { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve possuir 8 caracteres")]
        public string EntityCEP { get; set; }
        
        [Display(Name = "Rua")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityAvenue { get; set; }
        
        [Display(Name = "Numero")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityNumber { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityNeighborhood { get; set; }
        
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityCity { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Minimo {2} caracteres")]
        public string EntityState { get; set; }
        
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
        }
    }
}
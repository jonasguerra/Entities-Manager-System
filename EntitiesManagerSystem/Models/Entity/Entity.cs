using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome da Entidade")]
        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Minimo 5 caracteres")]
        public string EntityName { get; set; }

        [Display(Name = "Responsavel pela Entidade")]
        [Required(ErrorMessage = "Responsavel deve ser informado")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityResponsableName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail deve ser informado")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityEmail { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Telefone deve ser informado")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Apenas numeros devem ser informados")]
        [StringLength(11, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityPhone { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha deve ser informada")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityPassword { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "CEP deve ser informado")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve possuir 8 caracteres")]
        public string EntityCEP { get; set; }
        
        [Display(Name = "Rua")]
        [Required(ErrorMessage = "Rua deve ser informada")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityAvenue { get; set; }
        
        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Bairro deve ser informado")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string EntityNeighborhood { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Estado deve ser informado")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Minimo 4 caracteres")]
        public string EntityState { get; set; }
        
        [Display(Name = "Ponto de Referencia")]
        [Required(ErrorMessage = "Ponto de Referencia deve ser informado")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo 6 caracteres")]
        public string ReferencePoint { get; set; }
        
        [Display(Name = "Afinidade")]
        [Required(ErrorMessage = "Afinidade deve ser informada")]
        public string EntityAfinity { get; set; }
        
        [Display(Name = "Afinidade")]
        [Required(ErrorMessage = "Sigla deve ser informada")]
        public string EntityInitials { get; set; }
        
        [Display(Name = "Data de Criação")]
        [Required(ErrorMessage = "Data de Criação deve ser informada")]
        public DateTime EntityCreationDate { get; set; }
        
        [Display(Name = "Redes Sociais")]
        public string EntitySocialNetwork { get; set; }
        
        [Display(Name = "Site")]
        public string EntityWebSite { get; set; }
        
        [Display(Name = "Descrição")]
        public string EntityDescription { get; set; }
        
        [Display(Name = "Imagem da Entidade")]
        public string EntityPhotoImageName { get; set; }

        public Entity()
        {
        }
    }
}
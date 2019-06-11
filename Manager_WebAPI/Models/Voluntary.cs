using System;
using System.ComponentModel.DataAnnotations;

namespace Manager_API.Models.Voluntary
{
    public class Voluntary
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O {0} deve ser informado.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryName { get; set; }
        
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryEmail { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Apenas numeros devem ser informados")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryPhone { get; set; }

        [Required]
        [Display(Name = "Senha")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 8)]
        [RegularExpression(
            "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage =
                "As senhas devem ter pelo menos oito caracteres e conter pelo menos três das seguintes opções: maiúscula (A a Z), minúscula (a-z), número (0 a 9) e caractere especial (por exemplo, @ # $% ^ & *)")]
        [DataType(DataType.Password)]
        public string VoluntaryPassword { get; set; }

        [Display(Name = "Confirme a Senha")]
        [Compare("VoluntaryPassword", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string VoluntaryConfirmPassword { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "CEP deve possuir 8 caracteres")]
        public string VoluntaryCEP { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Minimo 8 caracteres")]
        public string VoluntaryAvenue { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryNumber { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryNeighborhood { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryCity { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Minimo {2} caracteres")]
        public string VoluntaryState { get; set; }
        
        [Display(Name = "Afinidade")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public string VoluntaryAffinity { get; set; }

        [Display(Name = "Redes Sociais")] 
        public string VoluntarySocialNetwork { get; set; }

        [Display(Name = "Imagem da Entidade")] 
        public string VoluntaryPhotoImageName { get; set; }

        public Voluntary()
        {
        }
    }
}
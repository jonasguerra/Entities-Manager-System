using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        
        public bool IsEntity { get; set; }
        public bool IsVoluntary { get; set; }
        public bool IsModerator { get; set; }
        
        public bool IsApproved { get; set; }
        
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Minimo {2} caracteres")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Senha")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 8)]
//        [RegularExpression(
//            "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
//            ErrorMessage =
//                "As senhas devem ter pelo menos oito caracteres e conter pelo menos três das seguintes opções: maiúscula (A a Z), minúscula (a-z), número (0 a 9) e caractere especial (por exemplo, @ # $% ^ & *)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirme a Senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
        public string Token { get; set; }
    }
}
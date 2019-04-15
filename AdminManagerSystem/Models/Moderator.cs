using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdminManagerSystem.Models
{
    public class Moderator
    {
        
//        Regex pro telefone
//        ^(\b|\({0})(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})(\-?|\s)(\d{4}))\b

//        https://liftcodeplay.com/2017/07/15/asp-net-core-password-complexity-validation-using-a-regular-expression-in-a-view-model/        



        public Guid Id { get; set; }
         
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string Usename { get; set; }
        
        
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string FirstName { get; set; }
        
        
        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string LastName { get; set; }
        
        
        [Required]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "As senhas devem ter pelo menos oito caracteres e conter pelo menos três das seguintes opções: maiúscula (A a Z), minúscula (a-z), número (0 a 9) e caractere especial (por exemplo, @ # $% ^ & *)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }

        public Moderator()
        {
            
        }


    }
}
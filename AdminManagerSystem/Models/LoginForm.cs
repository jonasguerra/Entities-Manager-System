using System;
using System.ComponentModel.DataAnnotations;

namespace AdminManagerSystem.Models
{
    public class LoginForm
    {
        public Guid Id { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O {0} deve ser informado")]
        public string Email { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A {0} deve ser informada")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginForm()
        {
        }
    }
}
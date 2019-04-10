using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class LoginForm
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O usuário deve ser informado")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "A senha deve ser informada")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public LoginForm()
        {
            
        }
    }
}
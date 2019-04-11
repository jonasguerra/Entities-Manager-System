using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class EntityRegisterForm
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O usuário deve ser informado")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "A senha deve ser informada")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string EntityName { get; set; }
        public string EntityResponsableName { get; set; }
        public string EntityEmail { get; set; }
        public string EntityPhone { get; set; }
        public string EntityPassword { get; set; }
        public string EntityZipCode { get; set; }
        public string EntityAvenue { get; set; }
        public string EntityNeighborhood{ get; set; }
        public string EntityState { get; set; }

        public EntityRegisterForm()
        {
            
        }
    }
}
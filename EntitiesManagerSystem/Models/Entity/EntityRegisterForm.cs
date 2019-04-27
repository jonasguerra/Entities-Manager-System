using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models.Entity
{
    public class EntityRegisterForm
    {
        
        [Required(ErrorMessage = "O usuário deve ser informado")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "A senha deve ser informada")]
        [Display(Name = "Senha")]
        
        
        public string EntityName { get; set; }
        public string EntityResponsableName { get; set; }
        public string EntityEmail { get; set; }
        public string EntityPhone { get; set; }
        public string EntityPassword { get; set; }
        public string EntityZipCode { get; set; }
        public string EntityAvenue { get; set; }
        public string EntityNeighborhood { get; set; }
        public string EntityStreetNumber { get; set; }
        public string EntityCity { get; set; }
        public string EntityState { get; set; }
        public string EntityReferencyPoint { get; set; }
        public string EntityActuationArea { get; set; }
        public string Entity { get; set; }


        public EntityRegisterForm()
        {
            
        }
    }
}
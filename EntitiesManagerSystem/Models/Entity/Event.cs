using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models.Entity
{
    public class Event
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "O {0} deve ser informado")]
        public string Title { get; set; }
    }
}
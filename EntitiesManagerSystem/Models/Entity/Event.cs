using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Models
{
    public class Event
    {
        public Guid EventId { get; set; }
        
        public Address Address { get; set; }
        
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {1} e {2} caractéres")]
        public string Title { get; set; }
        
        
        [Display(Name = "Descrição do evento")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        [StringLength(100000, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caractéres")]
        public string Description { get; set; }
        
        [Display(Name = "Data do Evento")]
        [Required(ErrorMessage = "{0} deve ser informada")]
        public DateTime Date { get; set; }
        
        public string Affinity { get; set; }
        
        public List<Affinity> Affinities { get; set; }
            
        public Event()
        {
        }
    }
}
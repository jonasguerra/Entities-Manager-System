using System;

namespace Manager_Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public bool IsEntity { get; set; }
        public bool IsVoluntary { get; set; }
        public bool IsModerator { get; set; }
        
        public bool IsApproved { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public bool PassswordIsValid(string password)
        {
            return (password == Password && IsApproved);
        }
    }
}
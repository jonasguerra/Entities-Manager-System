using System;
using Manager_Application.DTO;
using Manager_Domain.Entities;

namespace Manager_Application.Adapter
{
    public class UserAdapter
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO()
            {
                UserId = user.UserId,
                IsVoluntary = user.IsVoluntary,
                IsEntity  = user.IsEntity ,
                IsModerator = user.IsModerator,
                IsApproved = user.IsApproved,
                Email = user.Email,
                Password = user.Password
            };
        }
        
        
        public static User ToDomain(UserDTO user)
        {
            return new User()
            {
                UserId = user.UserId,
                IsVoluntary = user.IsVoluntary,
                IsEntity  = user.IsEntity ,
                IsModerator = user.IsModerator,
                IsApproved = user.IsApproved,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}
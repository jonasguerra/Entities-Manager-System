using System;
using System.Collections.Generic;
using Manager_Application.Adapter;
using Manager_Application.DTO;
using Manager_Domain.Entities;
using Manager_Domain.Interfaces;

namespace Manager_Application
{
    public class UserAplication
    {
        IUserRepository userRepository;

        public UserAplication(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public bool Delete(Guid id)
        {
            return userRepository.Delete(id);
        }
        
        public Guid Insert(UserDTO userDto)
        {
            userDto.UserId = Guid.NewGuid();
            userDto.IsApproved = true;
            userDto.IsModerator = true;
            var user = UserAdapter.ToDomain(userDto);
            return userRepository.Insert(user);
        }
        
        public UserDTO Autenticar(string email, string password)
        {
            var user = this.userRepository.Find(email.ToLower());
            if (user == null)
            {
                throw new ApplicationException("Usuario não encontrado");
            }

            if (!user.PassswordIsValid(password))
            {
                return null;
            }
            else
            {
                return UserAdapter.ToDTO(user);
            }
        }

        public void TrocarSenha(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ApplicationException("E-mail deve ser informado");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ApplicationException("Nova senha deve ser informada");
            }

            var user = this.userRepository.Find(email);
            user.Password = newPassword;

            this.userRepository.Update(user);
        }
        
        public UserDTO GetByEmail(string email)
        {
            var user = userRepository.Find(email);
            return UserAdapter.ToDTO(user);
        }
        
        public UserDTO Get(Guid id)
        {
            var user = userRepository.Find(id);
            return UserAdapter.ToDTO(user);
        }
        
        public List<UserDTO> GetAll()
        {
            List<UserDTO> usersDto = new List<UserDTO>();
            var users = userRepository.FindAll();
            foreach (User user in users)
            {
                usersDto.Add(UserAdapter.ToDTO(user));
            }
            return usersDto;
        }
    }
}
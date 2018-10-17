using BLLStandard.DTO;
using BLLStandard.ServicesInterfaces;
using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLLStandard.Services
{
    public class UserService : IUserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public void AddUser(UserDTO userDTO)
        {
            repository.AddUser(UserDTOToUser(userDTO));
        }

        public void AddUserRole(UserDTO userDTO, string roleName)
        {
            repository.AddUserRole(UserDTOToUser(userDTO), roleName);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var list = repository.GetAll();

            var userDTOs = new List<UserDTO>();
            foreach (var user in list)
            {
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    Surname = user.Surname,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Account = user.Account,
                    TimeZone = user.TimeZone,
                    Language = user.Language
                });
            }

            return userDTOs;
        }

        public IEnumerable<string> GetAllRoles()
        {
            return repository.GetAllRoles();
        }

        public UserDTO GetUserById(string id)
        {
            var user = repository.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            return UserToUserDTO(user);
        }

        public UserDTO GetUserByName(string userName)
        {
            var user = repository.GetUserByName(userName);

            if (user == null)
            {
                return null;
            }

            return UserToUserDTO(user);
        }

        public IEnumerable<string> GetUserRoles(string userName)
        {
            return repository.GetUserRoles(userName);
        }

        public void UpdateUser(UserDTO userDTO)
        {
            repository.UpdateUser(UserDTOToUser(userDTO));
        }

        private User UserDTOToUser(UserDTO userDTO)
        {
            return new User
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                FirstName = userDTO.FirstName,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                Account = userDTO.Account,
                TimeZone = userDTO.TimeZone,
                Language = userDTO.Language
            };
        }

        private UserDTO UserToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Account = user.Account,
                TimeZone = user.TimeZone,
                Language = user.Language
            };
        }

        public void DeleteUserFromRole(string userId)
        {
            if (userId != null)
                repository.DeleteUserFromRole(userId);
        }

        public void Delete(string userId)
        {
            if (userId != null)
                repository.Delete(userId);
        }
    }
}

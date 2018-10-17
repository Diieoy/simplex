using BLLStandard.DTO;
using System.Collections.Generic;

namespace BLLStandard.ServicesInterfaces
{
    public interface IUserService
    {
        UserDTO GetUserByName(string userName);

        UserDTO GetUserById(string id);

        void AddUser(UserDTO user);

        void UpdateUser(UserDTO user);

        void AddUserRole(UserDTO userDTO, string roleName);

        void DeleteUserFromRole(string userId);

        void Delete(string userId);

        IEnumerable<string> GetUserRoles(string userName);

        IEnumerable<UserDTO> GetAllUsers();

        IEnumerable<string> GetAllRoles();
    }
}

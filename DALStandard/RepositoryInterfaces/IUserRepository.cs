using DALStandard.Models;
using System.Collections.Generic;

namespace DALStandard.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetUserByName(string userName);

        User GetUserById(string id);

        void AddUser(User user);

        void UpdateUser(User user);

        void AddUserRole(User user, string roleName);

        void DeleteUserFromRole(string userId);

        void Delete(string userId);

        IEnumerable<string> GetUserRoles(string userName);

        IEnumerable<User> GetAll();

        IEnumerable<string> GetAllRoles();
    }
}

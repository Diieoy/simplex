using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.DTO;

namespace WcfServiceLibrary.ServicesInterfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        UserDTO GetUserByName(string userName);

        [OperationContract]
        UserDTO GetUserById(string id);

        [OperationContract]
        void AddUser(UserDTO user);

        [OperationContract]
        void UpdateUser(UserDTO user);

        [OperationContract]
        void AddUserRole(UserDTO userDTO, string roleName);

        [OperationContract]
        IEnumerable<string> GetUserRoles(string userName);
    }
}

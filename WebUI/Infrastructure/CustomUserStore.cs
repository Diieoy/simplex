using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using BLLStandard.ServicesInterfaces;
using BLLStandard.DTO;

namespace WebUI.Infrastructure
{
    public class CustomUserStore : IUserClaimStore<UserDTO>, IUserPasswordStore<UserDTO>, IUserRoleStore<UserDTO>
    {
        private IUserService _userService;

        public CustomUserStore(IUserService userService)
        {
            _userService = userService;
        }

        public Task AddClaimAsync(UserDTO user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserDTO user)
        {
            var userDTO = user;
            userDTO.Id = Guid.NewGuid().ToString();
            _userService.AddUser(userDTO);
            AddToRoleAsync(userDTO, "user");

            return Task.FromResult(0);
        }

        public Task DeleteAsync(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<UserDTO> FindByIdAsync(string userId)
        {
            var userDto = _userService.GetUserById(userId);
            var result = userDto != null ? userDto : null;
            return Task.FromResult(result);
        }

        public Task<UserDTO> FindByNameAsync(string userName)
        {
            var userDto = _userService.GetUserByName(userName);
            var result = userDto != null ? userDto : null;
            return Task.FromResult(result);
        }

        public Task<IList<Claim>> GetClaimsAsync(UserDTO user)
        {
            var roles = this._userService.GetUserRoles(user.UserName);

            IList<Claim> claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var u = _userService.GetUserById(user.Id);
            if (u != null)
            {
                claims.Add(new Claim(ClaimTypes.UserData, u.Language));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.UserData, "en"));
            }

            return Task.FromResult(claims);

        }

        public Task<string> GetPasswordHashAsync(UserDTO user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(UserDTO user)
        {
            var userDto = _userService.GetUserById(user.Id);
            return Task.FromResult(userDto.PasswordHash != null);
        }

        public Task RemoveClaimAsync(UserDTO user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(UserDTO user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task UpdateAsync(UserDTO user)
        {
            var userFromDb = _userService.GetUserByName(user.UserName);
            _userService.UpdateUser(user);

            return Task.FromResult(0);
        }

        public Task AddToRoleAsync(UserDTO user, string roleName)
        {
            _userService.AddUserRole(user, roleName);
            return Task.FromResult(0);
        }

        public Task RemoveFromRoleAsync(UserDTO user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(UserDTO user)
        {
            var roles = (IList<string>)_userService.GetUserRoles(user.UserName);
            return Task.FromResult(roles);
        }

        public Task<bool> IsInRoleAsync(UserDTO user, string roleName)
        {
            int num = 0;

            var roles = _userService.GetUserRoles(user.UserName);

            foreach (var item in roles)
            {
                if (item == roleName)
                {
                    num++;
                }
            }

            return Task.FromResult(num > 0);
        }
    }
}
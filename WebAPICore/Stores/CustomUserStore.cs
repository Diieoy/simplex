using BLLStandard.DTO;
using BLLStandard.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPICore.Stores
{
    public class CustomUserStore : IUserClaimStore<UserDTO>, IUserPasswordStore<UserDTO>, IUserRoleStore<UserDTO>
    {
        private IUserService userService;

        public CustomUserStore(IUserService userService)
        {
            this.userService = userService;
        }

        public Task AddClaimsAsync(UserDTO user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(UserDTO user, string roleName, CancellationToken cancellationToken)
        {
            userService.AddUserRole(user, roleName);
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> CreateAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userDTO = user;
            userDTO.Id = Guid.NewGuid().ToString();
            userService.AddUser(userDTO);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(UserDTO user, CancellationToken cancellationToken)
        {
            userService.Delete(user.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public Task<UserDTO> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var userDTO = userService.GetUserById(userId);
            var result = userDTO != null ? userDTO : null;
            return Task.FromResult(result);
        }

        public Task<UserDTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var userDTO = userService.GetUserByName(normalizedUserName);
            var result = userDTO != null ? userDTO : null;
            return Task.FromResult(result);
        }

        public Task<IList<Claim>> GetClaimsAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var roles = userService.GetUserRoles(user.UserName);

            IList<Claim> claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            var u = userService.GetUserById(user.Id);
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

        public Task<string> GetNormalizedUserNameAsync(UserDTO user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var roles = (IList<string>)userService.GetUserRoles(user.UserName);
            return Task.FromResult(roles);
        }

        public Task<string> GetUserIdAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(UserDTO user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<IList<UserDTO>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserDTO>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userDTO = userService.GetUserById(user.Id);
            return Task.FromResult(userDTO.PasswordHash != null);
        }

        public Task<bool> IsInRoleAsync(UserDTO user, string roleName, CancellationToken cancellationToken)
        {
            int num = 0;

            var roles = userService.GetUserRoles(user.UserName);

            foreach (var item in roles)
            {
                if (item == roleName)
                {
                    num++;
                }
            }

            return Task.FromResult(num > 0);
        }

        public Task RemoveClaimsAsync(UserDTO user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserDTO user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(UserDTO user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserDTO user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(UserDTO user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(UserDTO user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(UserDTO user, CancellationToken cancellationToken)
        {
            var userFromDb = userService.GetUserByName(user.UserName);
            userService.UpdateUser(user);

            return IdentityResult.Success;
        }
    }
}

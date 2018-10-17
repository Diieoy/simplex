using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLLStandard.DTO;
using BLLStandard.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPICore.Models;

namespace WebAPICore.Controllers
{    
    public class AccountController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly UserManager<UserDTO> userManager;
        private readonly SignInManager<UserDTO> signInManager;

        public AccountController(
            IConfiguration configuration, 
            IUserService userService,
            UserManager<UserDTO> userManager,
            SignInManager<UserDTO> signInManager)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }        

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <remarks>This method returns all roles.</remarks>
        /// <response code="200">Roles returned.</response>
        /// <response code="400">Something wrong.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/getAllRoles")]
        [HttpGet]
        public async Task<List<string>> GetAllRoles()
        {
            return await Task.Run(() => userService.GetAllRoles().ToList());
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <remarks>This method returns all users.</remarks>
        /// <response code="200">User returned.</response>
        /// <response code="400">Something wrong.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/getAllUsers")]
        [HttpGet]
        public async Task<List<NewUserModel>> GetAllUsers()
        {
            var users = userService.GetAllUsers().ToList();
            var usersWithRoles = new List<NewUserModel>();

            foreach (var item in users)
            {
                usersWithRoles.Add(new NewUserModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    PasswordHash = item.PasswordHash,
                    FirstName = item.FirstName,
                    Surname = item.Surname,
                    Email = item.Email,
                    TimeZone = item.TimeZone,
                    UserLanguage = (UserLanguage)Enum.Parse(typeof(UserLanguage), item.Language),
                    Role = userService.GetUserRoles(item.UserName).FirstOrDefault()
                });
            }

            return await Task.Run(() => usersWithRoles);
        }

        /// <summary>
        /// Register a user with a role.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <remarks>Registration a user with a role.</remarks>
        /// <response code="200">User registered.</response>
        /// <response code="400">Wrong credentials.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/registerWithRole")]
        [HttpPost]
        public async Task<object> RegisterWithRole([FromBody] NewUserModel model)
        {
            var stringBuilder = new StringBuilder();

            var user = new UserDTO
            {
                UserName = model.UserName,
                PasswordHash = model.PasswordHash,
                Email = model.Email,
                FirstName = model.FirstName,
                Surname = model.Surname,
                TimeZone = model.TimeZone,
                Language = model.UserLanguage.ToString(),
                Account = 0
            };

            IdentityResult result2 = null;
            var result = await userManager.CreateAsync(user, model.PasswordHash);
            if (result.Succeeded)
            {
                var userFromDb = await userManager.FindByNameAsync(model.UserName);
                result2 = await userManager.AddToRoleAsync(userFromDb, model.Role);
            }
            

            if (result.Succeeded && result2.Succeeded)
            {
                return await GenerateJwtToken(model.UserName, model.PasswordHash);
            }

            var errors = result.Errors?.Select(e => e.Description).ToArray();
            if (errors?.Any() is true)
            {
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine(error);
                }
            }

            return BadRequest(stringBuilder.ToString());
        }

        /// <summary>
        /// Login method.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <remarks>The login method.</remarks>
        /// <response code="200">User signed in.</response>
        /// <response code="400">Wrong credentials.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Route("api/login")]
        [HttpPost]        
        public async Task<object> Login([FromBody] LoginModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await userManager.FindByNameAsync(model.UserName);
                return await GenerateJwtToken(model.UserName, model.Password);
            }

            return BadRequest("INVALID_LOGIN_ATTEMPT");
        }

        /// <summary>
        /// Get user role.
        /// </summary>
        /// <param name="userName">
        /// The model.
        /// </param>
        /// <remarks>This method returns a user role</remarks>
        /// <response code="200">Role returned.</response>
        /// <response code="400">Incorrect name.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/getUserRole/{userName}")]
        [HttpGet]
        public async Task<string> GetRole(string userName)
        {
            var user = userService.GetUserByName(userName);
            var userRole = await userManager.GetRolesAsync(user);

            return userRole.FirstOrDefault();
        }

        /// <summary>
        /// Get info about a user.
        /// </summary>
        /// <param name="userName">
        /// The model.
        /// </param>
        /// <remarks>This method returns information about user.</remarks>
        /// <response code="200">Information returned.</response>
        /// <response code="400">Incorrect name.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [HttpGet("api/userinfo/{userName}")]
        public async Task<object> GetUserInfo(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var role = userService.GetUserRoles(userName);

            UserInfoModel info = new UserInfoModel()
            {
                Id = user.Id,
                UserName = userName,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Email = user.Email,
                TimeZone = user.TimeZone,
                UserLanguage = (UserLanguage)Enum.Parse(typeof(UserLanguage), user.Language),
                Role = role.FirstOrDefault()
            };

            return info;
        }

        /// <summary>
        /// Get user account.
        /// </summary>
        /// <param name="userDTO">
        /// The model.
        /// </param>
        /// <remarks>This method returns user account</remarks>
        /// <response code="200">Account returned.</response>
        /// <response code="400">Incorrect dto.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/account")]
        [HttpPut]
        public async Task<UserDTO> Account([FromBody] UserDTO userDTO)
        {
            var user = await userManager.FindByIdAsync(userDTO.Id);

            user.Account += userDTO.Account;

            userService.UpdateUser(user);

            return user;
        }

        /// <summary>
        /// Change password method.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <remarks>This method changes the password</remarks>
        /// <response code="200">Password changer.</response>
        /// <response code="400">Incorrect model.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/changePassword")]
        [HttpPut]
        public async Task<object> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return null;
            }

            return BadRequest("INVALID_PASSWORD");
        }

        /// <summary>
        /// Get information about user.
        /// </summary>
        /// <param name="userName">
        /// The model.
        /// </param>
        /// <remarks>This method returned an information about user.</remarks>
        /// <response code="200">Information returned.</response>
        /// <response code="400">Incorrect name.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [HttpGet("api/edit/{userName}")]
        public async Task<RegisterModel> GetInformation(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            return new RegisterModel
            {
                UserName = userName,
                Password = user.PasswordHash,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Email = user.Email,
                TimeZone = user.TimeZone,
                UserLanguage = (UserLanguage)Enum.Parse(typeof(UserLanguage), user.Language),                
            };
        }

        /// <summary>
        /// Delete the user.
        /// </summary>
        /// <remarks>This method delete the user.</remarks>
        /// <response code="200">User is deleted.</response>
        /// <response code="400">Something wrong.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/delete/{userId}")]
        [HttpGet]
        public Task DeleteUser(string userId)
        {
            var user = userManager.FindByIdAsync(userId).Result;
            userManager.DeleteAsync(user);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Edit user information.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <remarks>This method edits information about user.</remarks>
        /// <response code="200">Information edited.</response>
        /// <response code="400">Incorrect model.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/edit")]
        [HttpPut]
        public async Task<UserDTO> Edit([FromBody] RegisterModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.TimeZone = model.TimeZone;
            user.Language = model.UserLanguage.ToString();

            userService.UpdateUser(user);

            return user;
        }

        /// <summary>
        /// Edit user information and his role.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <remarks>This method edits information about user.</remarks>
        /// <response code="200">User edited.</response>
        /// <response code="400">Incorrect model.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Authorize]
        [Route("api/editWithRole")]
        [HttpPut]
        public async Task<NewUserModel> EditWithRole([FromBody] NewUserModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.TimeZone = model.TimeZone;
            user.Language = model.UserLanguage.ToString();

            userService.DeleteUserFromRole(user.Id);
            userService.AddUserRole(user, model.Role);
            userService.UpdateUser(user);

            return new NewUserModel { Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Email = user.Email,
                Role = model.Role,
                TimeZone = user.TimeZone,
                UserLanguage = (UserLanguage)Enum.Parse(typeof(UserLanguage), user.Language),                
            };
        }

        /// <summary>
        /// Register user with role "user".
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <remarks>This method registers a user</remarks>
        /// <response code="200">User registered.</response>
        /// <response code="400">Incorrect model.</response>
        /// <response code="500">Oops! Something went wrong</response>
        [Route("api/register")]
        [HttpPost]
        public async Task<object> Register([FromBody] UserModel model)
        {
            var stringBuilder = new StringBuilder();

            var user = new UserDTO
            {
                UserName = model.UserName,
                PasswordHash = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                Surname = model.Surname,
                TimeZone = model.TimeZone,
                Language = model.UserLanguage.ToString(),
                Account = 0
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");
                return await GenerateJwtToken(model.UserName, model.Password);
            }

            var errors = result.Errors?.Select(e => e.Description).ToArray();
            if (errors?.Any() is true)
            {
                foreach (var error in errors)
                {
                    stringBuilder.AppendLine(error);
                }
            }

            return BadRequest(stringBuilder.ToString());
        }        

        private async Task<object> GenerateJwtToken(string userName, string password)
        {
            var user = userService.GetUserByName(userName);
            var userRole = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userService.GetUserByName(userName).Id)
            };
            claims.AddRange(userRole.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(configuration.GetValue<double>("JwtExpireDays"));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
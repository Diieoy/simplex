using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WpfApp.DTO;
using WpfApp.Models.Services;

namespace WpfApp.Models
{
    public class UserService
    {
        static HttpClient client;
        static BindableCollection<UserModel> users = new BindableCollection<UserModel>();
        private BindableCollection<string> allRoles = new BindableCollection<string>();

        public BindableCollection<UserModel> Users { get { return users; } set { users = value; } }
        public BindableCollection<string> AllRoles { get { return allRoles; } set { allRoles = value; } }

        static async Task<string> TryLoginAsync(LoginModel model)
        {
            var response = await client.PostAsJsonAsync("api/login", model);
            
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }

            var token = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

            return token;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = await TryLoginAsync(model);

                return token;
            }
        }

        static async Task<string> TryToGetUserRoleAsync(string userName, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("api/getUserRole/" + userName);
                var role = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

                return role;
            }
        }

        public async Task<string> GetUserRoleAsync(string userName, string token)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                

                var role = await TryToGetUserRoleAsync(userName, token);

                return role;
            }
        }

        static async Task<UserModel> UpdateUserAsync(UserModel model)
        {
            var response = await client.PutAsJsonAsync("api/editWithRole", model);
            var updModel = JsonConvert.DeserializeObject<UserModel>(await response.Content.ReadAsStringAsync());

            return updModel;
        }

        public async Task<UserModel> UpdateAsync(UserModel model)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserInfo.Token);

                var updUser = await UpdateUserAsync(model);

                return updUser;
            }
        }

        static async Task<List<string>> GetAllRolesAsync()
        {
            var response = await client.GetAsync("api/getAllRoles");
            var result = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task LoadRolesAsync()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserInfo.Token);

                AllRoles.Clear();
                var roles = await GetAllRolesAsync();

                AllRoles.AddRange(roles);
            }
        }

        static async Task<List<UserModel>> GetAllUsersAsync()
        {
            var response = await client.GetAsync("api/getAllUsers");
            var result = JsonConvert.DeserializeObject<List<UserModel>>(await response.Content.ReadAsStringAsync());            

            return result;
        }        

        public async Task LoadUsersAsync()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserInfo.Token);

                this.Users.Clear();
                var users = await GetAllUsersAsync();

                Users.AddRange(users.OrderBy(x => x.UserName));
            }
        }

        static async Task<string> RegisterUserAsync(UserModel model)
        {
            var response = await client.PostAsJsonAsync("api/registerWithRole", model);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "USERNAME_ALREADY_EXISTS";
            }

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var token = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

            return token;
        }

        public async Task<string> RegisterUser(UserModel model)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserInfo.Token);

                var userToken = await RegisterUserAsync(model);

                return userToken;
            }
        }

        static Task DeleteUser(string userId)
        {
            var response = client.GetAsync("api/delete/" + userId).Result;

            return Task.FromResult(0);
        }

        public Task Delete(string userId)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserInfo.Token);

                DeleteUser(userId);

                return Task.FromResult(0);
            }
        }
    }
}

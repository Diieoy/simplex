using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleAppHost
{
    public class UserPasswordValidator : UserNamePasswordValidator
    {
        public static string Role;

        public override void Validate(string userName, string password)
        {
            var role = GetUserRoleAsync(userName, password).Result;
            if (role != null)
            {
                Role = role;
                return;
            }

            throw new SecurityTokenValidationException();
        }

        static async Task<string> GetUserRoleAsync(string userName, string token)
        {
            using(var client = new HttpClient())
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
    }
}

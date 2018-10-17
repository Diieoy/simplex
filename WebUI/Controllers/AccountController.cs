using WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLLStandard.DTO;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager _authenticationManager => HttpContext.GetOwinContext().Authentication;
        private UserManager<UserDTO, string> _userManager;
        static HttpClient client;

        public AccountController(UserManager<UserDTO, string> userManager)
        {
            _userManager = userManager;
            _userManager.PasswordValidator = new MinimumLengthValidator(1);
            _userManager.UserValidator = new UserValidator<UserDTO>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        static async Task<UserInfoModel> GetUserInfoAsync(string userName)
        {
            var response = await client.GetAsync("api/userinfo/" + userName);
            var user = JsonConvert.DeserializeObject<UserInfoModel>(await response.Content.ReadAsStringAsync());

            return user;
        }

        static async Task<string> RegisterWebApiAsync(RegisterViewModel model)
        {
            var response = await client.PostAsJsonAsync("api/register", model);
            var token = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

            return token;
        }

        static async Task<UserDTO> AddMoneyAsync(UserDTO userDTO)
        {
            var response = await client.PutAsJsonAsync("api/account", userDTO);
            var user = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());

            return user;
        }

        static async Task<RegisterViewModel> EditAsync(RegisterViewModel model)
        {
            var response = await client.PutAsJsonAsync("api/edit", model);
            var updModel = JsonConvert.DeserializeObject<RegisterViewModel>(await response.Content.ReadAsStringAsync());

            return updModel;
        }

        static async Task<RegisterViewModel> GetEditModelAsync(string userName)
        {
            var response = await client.GetAsync("api/edit/" + userName);
            var model = JsonConvert.DeserializeObject<RegisterViewModel>(await response.Content.ReadAsStringAsync());

            return model;
        }

        static async Task<bool> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var response = await client.PutAsJsonAsync("api/changePassword", model);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        static async Task<string> LoginWebApiAsync(LoginViewModel model)
        {
            var response = await client.PostAsJsonAsync("api/login", model);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }

            var token = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());

            return token;
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var identity = (ClaimsIdentity)User.Identity;
                var token = identity.FindFirst(ClaimTypes.Authentication).Value;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                model.UserName = User.Identity.GetUserName();

                var result = await ChangePasswordAsync(model);
                if(result == false)
                {
                    ViewBag.ErrMessage = "Error";
                    return View(model);
                }

                ViewBag.Message = "Changed";

                return View();
            }
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }                
        
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = await LoginWebApiAsync(model);

                if(token == null)
                {
                    ModelState.AddModelError(string.Empty, "Error");
                    return View();
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var userInfo = await GetUserInfoAsync(model.UserName);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userInfo.Id));
                claims.Add(new Claim(ClaimTypes.Name, userInfo.UserName));
                claims.Add(new Claim(ClaimTypes.Role, userInfo.Role));
                claims.Add(new Claim(ClaimTypes.UserData, userInfo.UserLanguage.ToString()));
                claims.Add(new Claim(ClaimTypes.Authentication, token));

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                _authenticationManager.SignOut();
                _authenticationManager.SignIn(identity);

                var language = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;

                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = language;
                Response.Cookies.Add(cookie);
                
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            Session["cart"] = null;
            Session["userAccount"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = await RegisterWebApiAsync(model);

                await Login(new LoginViewModel() { UserName = model.UserName, Password = model.Password });                
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Account()
        {
            return View(_userManager.FindByName(User.Identity.Name));
        }

        [HttpPost]
        public async Task<ActionResult> Account(UserDTO userDTO)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var identity = (ClaimsIdentity)User.Identity;
                var token = identity.FindFirst(ClaimTypes.Authentication).Value;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var user = await AddMoneyAsync(userDTO);

                return View(user);
            }
        }

        public async Task<ActionResult> Edit()
        {           
            var userDTO = _userManager.FindByName(User.Identity.Name);

            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var identity = (ClaimsIdentity)User.Identity;
                var token = identity.FindFirst(ClaimTypes.Authentication).Value;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var model = await GetEditModelAsync(userDTO.UserName);

                return View(model);
            }            
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (client = new HttpClient())
            {
                var identity = (ClaimsIdentity)User.Identity;
                var token = identity.FindFirst(ClaimTypes.Authentication).Value;

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddressForClient"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var updModel = await EditAsync(model);

                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = model.UserLanguage.ToString();
                Response.Cookies.Add(cookie);

                ViewBag.Message = Resources.AccountControllerTexts.Changed;

                return View(updModel);
            }           
        }
    }
}
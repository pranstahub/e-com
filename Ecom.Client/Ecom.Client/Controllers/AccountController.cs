using Ecom.Client.Core.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ecom.Client.DTO.Request;
using Ecom.Client.DTO.Response;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Ecom.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericHttpClient _httpClient;

        public AccountController(IGenericHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInDTORequest model)
        {
            var result = await _httpClient.PostByAsync<SignInDTOResponse>(ApiConstants.CheckLogin, model);
            if (result != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim("UserId", result.id));
                identity.AddClaim(new Claim("CustomerId", result.customerId.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, result.email));
                identity.AddClaim(new Claim("UserName", result.userName));

                HttpContext.Session.SetString("Email", result.email);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                });

                if (HttpContext.Session.GetString("RegisteredStatus") == "False")
                {
                    return RedirectToAction("Create", "Customer");
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTORequest model)
        {
            if(ModelState.IsValid)
            {
                var result = await _httpClient.PostByAsync<OkObjectResult>(ApiConstants.RegisterUser, model);
                if (result != null)
                {
                    HttpContext.Session.SetString("Email", model.UserName);

                    HttpContext.Session.SetString("RegisteredStatus", "False");
                    return RedirectToAction("Create", "Customer");
                }
                return View();

            }
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

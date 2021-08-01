using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreCookiesAuthDemo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreCookiesAuthDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            var dbUser = await _authService.ValidateUser(username, password);

            if (dbUser != null)
            {
                // Create Claims 
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, dbUser.FirstName + " " + dbUser.LastName));
                claims.Add(new Claim("username", dbUser.UserName));

                //if (dbUser.UserName == "waqas")
                //{
                //    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                //} 

                // Create Identity
                var claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);

                // Create Principal 
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign In
                await HttpContext.SignInAsync(claimsPrincipal);

                // Redirect
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Admin");
            }

            TempData["ErrorMessage"] = "Invalid username or password.";
            return View("login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        [HttpGet("denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

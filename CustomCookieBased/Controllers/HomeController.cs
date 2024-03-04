using CustomCookieBased.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CustomCookieBased.Context;
using Microsoft.AspNetCore.Authorization;

namespace CustomCookieBased.Controllers
{
    public class HomeController : Controller
    { 
        private readonly CookieContext _context;

        public HomeController(CookieContext context)
        {
            _context = context;
        }

        public IActionResult SignIn()
        {
            return View(new UserSignInModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);    

            if (user != null) 
            {
                var roles = _context.Roles.Where(x => x.UserRoles.Any(x => x.UserId == user.Id)).Select(x => x.Definition).ToList();

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.UserName) };
                foreach (var role in roles) 
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin, Member")]
        public IActionResult Member()
        {
            return View();
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }

        

    }
}

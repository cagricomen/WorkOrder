using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkOrder.Core.Entities;
using WorkOrder.Data;

namespace WorkOrder.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly AppDbContext appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user )
        {
            var asa = appDbContext.Users.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if(asa != null && asa.UserRole.ToString().Equals("Admin"))
            {
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                userClaims.Add(new Claim(ClaimTypes.UserData, asa.UserName));
                var claimsIdentity = new ClaimsIdentity(userClaims,             CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Admin");
            }
            if (asa != null && asa.UserRole.ToString().Equals("User"))
            {
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.Role, "User"));
                userClaims.Add(new Claim(ClaimTypes.UserData, asa.UserName));
                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "User");
            }
            if (asa != null && asa.UserRole.ToString().Equals("Manager"))
            {
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.Role, "Manager"));
                userClaims.Add(new Claim(ClaimTypes.UserData, asa.UserName));
                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Manager");
            }

            else
            {
                return View();
            }
        }
    }
}

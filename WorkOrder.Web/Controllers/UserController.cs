using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkOrder.Business.Managers;
using WorkOrder.Data;
using WorkOrder.Data.Repositories;
using WorkOrder.Web.Attributes;

namespace WorkOrder.Web.Controllers
{
    [Authorize]
    [ClaimRequirement("User")]
    public class UserController : Controller
    {
        private readonly UserManager _userManager;

        public UserController( UserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult HomePage()
        {
            return View();
        }


        public async Task<IActionResult> Index()
        {
            var claimTypes = HttpContext.User.Claims.ToList();
            var userName = claimTypes[1].Value;
            var res = await _userManager.UserWorkOrder(userName);
            return View(res);
        }

        public async Task<IActionResult> StartWorkOrder(int id)
        {
            var result = await _userManager.StartWorkOrder(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<string> GetUser()
        {
            var claimTypes = HttpContext.User.Claims.ToList();
            var userName = claimTypes[1].Value;
            var user = await _userManager.UserFullName(userName);
            return user;
        }

        public async Task<IActionResult> Quit()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}

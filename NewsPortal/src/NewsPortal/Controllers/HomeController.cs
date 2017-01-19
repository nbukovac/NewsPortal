using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Data;
using NewsPortal.Interfaces;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            await Seed();
            return View(await _categoryRepository.GetAll());
        }

        public IActionResult PromoteUser()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task Seed()
        {
            var test = await _userManager.FindByNameAsync("uber.user@live.com");

            if (test != null && test.UserName == "uber.user@live.com")
            {
                return;
            }

            var user = new ApplicationUser()
            {
                UserName = "uber.user@live.com",
                Email = "uber.user@live.com"
            };

            await _userManager.CreateAsync(user, "#1Webapp");
        }
    }
}
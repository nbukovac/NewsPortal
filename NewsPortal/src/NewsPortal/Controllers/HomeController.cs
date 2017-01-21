using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ICategoryRepository categoryRepository, UserManager<ApplicationUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            //await Seed();

            var categories = await _categoryRepository.GetAll();
            var viewModel = new List<FrontPageArticlesViewModel>();

            foreach (var category in categories)
            {
                viewModel.Add(new FrontPageArticlesViewModel
                {
                    Category = category,
                    Trending =
                        await _categoryRepository.GetTrendingArticles(category.CategoryId, Constants.ArticlesNumber),
                    Newest = await _categoryRepository.GetNewestArticles(category.CategoryId, Constants.ArticlesNumber)
                });
            }

            return View(viewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users.ToList();
            var viewModel = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                viewModel.Add(new UserRoleViewModel
                {
                    UserId = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Role = roles.FirstOrDefault() == null ? "" : roles.First()
                });
            }

            return View(viewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public async Task<IActionResult> PromoteUser(Guid userId)
        {
            var user = _userManager.Users.First(m => m.Id == userId.ToString());
            await _userManager.AddToRoleAsync(user, Constants.AutorRole);

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task Seed()
        {
            var test = await _userManager.FindByNameAsync("uber.user@live.com");

            if ((test != null) && (test.UserName == "uber.user@live.com"))
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = "uber.user@live.com",
                Email = "uber.user@live.com"
            };

            await _userManager.CreateAsync(user, "#1Webapp");
            await _userManager.AddToRoleAsync(user, Constants.AdministratorRole);
        }
    }
}
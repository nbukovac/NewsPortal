using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticlesController(IArticleRepository articleRepository, UserManager<ApplicationUser> userManager,
            ICategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> ReadArticle(Guid articleId)
        {
            var article = _articleRepository.GetById(articleId);
            ViewBag.Autor = await _userManager.FindByIdAsync(article.UserId.ToString());

            return View(article);
        }

        public async Task<IActionResult> WriteArticle()
        {
            ViewBag.AutorId = await GetActiveUserId();
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteArticle(AddArticleViewModel addArticleViewModel)
        {
            if (ModelState.IsValid)
            {
                _articleRepository.Insert(addArticleViewModel);
                return RedirectToAction("ReadArticle");
            }

            ViewBag.AutorId = await GetActiveUserId();
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name",
                addArticleViewModel.CategoryId);

            return View(addArticleViewModel);
        }

        public async Task<IActionResult> EditArticle(Guid articleId)
        {
            var article = _articleRepository.GetById(articleId);
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name",
                article.CategoryId);

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleRepository.Update(article);
                return RedirectToAction("ReadArticle");
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name",
                article.CategoryId);

            return View(article);
        }

        public IActionResult DeleteArticle(Guid articleid)
        {
            _articleRepository.Delete(articleid);
            return RedirectToAction("Index", "Category");
        }

        private async Task<Guid> GetActiveUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Guid.Parse(user.Id);
        }
    }
}
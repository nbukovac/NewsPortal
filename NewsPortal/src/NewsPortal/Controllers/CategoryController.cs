using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;
using Sakura.AspNetCore;

namespace NewsPortal.Controllers
{   
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public IActionResult Create(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Insert(addCategoryViewModel);
                return RedirectToAction("Index", "Home");
            }

            return View(addCategoryViewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult Edit(Guid id)
        {
            return View(_categoryRepository.GetById(id));
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction("Index", "Home");
            }

            return View(category);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult Delete(Guid id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> CategoryArticles(Guid categoryId, bool trending = true, int page = 1)
        {
            List<Article> articles;
            var category = _categoryRepository.GetById(categoryId);

            if (trending)
            {
                articles = await _categoryRepository.GetTrendingArticles(categoryId, category.Articles.Count);
            }
            else
            {
                articles = await _categoryRepository.GetNewestArticles(categoryId, category.Articles.Count);
            }

            ViewBag.Trending = trending;
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = _categoryRepository.GetById(categoryId).Name;

            return View(articles.ToPagedList(Constants.PageSize, page));
        }
    }
}
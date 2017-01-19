using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;
using Sakura.AspNetCore;

namespace NewsPortal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public IActionResult Edit(Guid id)
        {
            return View(_categoryRepository.GetById(id));
        }

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

        public IActionResult Delete(Guid id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }

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
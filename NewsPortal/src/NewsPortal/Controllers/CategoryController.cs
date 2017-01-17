using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Repositories;

namespace NewsPortal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Insert(category);
                return RedirectToAction("Index");
            }

            return View(category);
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
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Delete(Guid id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult CategoryArticles(Guid categoryId)
        {
            return View(_categoryRepository.GetCategoryArticles(categoryId));
        }
    }
}
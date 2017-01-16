using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Interfaces;
using NewsPortal.Models;

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

        public IActionResult CategoryArticles(Guid categoryId)
        {
            return View(_categoryRepository.GetCategoryArticles(categoryId));
        }
    }
}
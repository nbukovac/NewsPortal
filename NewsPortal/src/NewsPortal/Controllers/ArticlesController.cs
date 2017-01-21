using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleVoteRepository _articleVoteRepository;

        private const string ConcatenatedRoles = Constants.AdministratorRole + "," + Constants.AutorRole;

        public ArticlesController(IArticleRepository articleRepository, UserManager<ApplicationUser> userManager,
            ICategoryRepository categoryRepository, ICommentRepository commentRepository,
            IArticleVoteRepository articleVoteRepository)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _articleVoteRepository = articleVoteRepository;
        }

        public async Task<IActionResult> ReadArticle(Guid articleId)
        {
            var article = _articleRepository.GetById(articleId);
            var userId = await GetActiveUserId();

            ViewBag.UserVote = _articleVoteRepository.GetUsersVote(articleId, userId);
            ViewBag.Autor = await _userManager.FindByIdAsync(article.UserId.ToString());

            return View(article);
        }

        public async Task<IActionResult> UpvoteArticle(Guid articleId)
        {
            var userId = await GetActiveUserId();
            var userVote = _articleVoteRepository.GetUsersVote(articleId, userId);
            bool increment = userVote != null;

            if (userVote == null)
            {
                userVote = new ArticleVote(userId, articleId);
                userVote.Upvoted();
                _articleVoteRepository.Insert(userVote);
            }
            else
            {
                userVote.Upvoted();
                _articleVoteRepository.Update(userVote);
            }

            _articleRepository.Upvote(articleId, increment);

            return RedirectToAction("ReadArticle", new { articleId = articleId });
        }

        public async Task<IActionResult> DownvoteArticle(Guid articleId)
        {
            var userId = await GetActiveUserId();
            var userVote = _articleVoteRepository.GetUsersVote(articleId, userId);
            bool decrement = userVote != null;

            if (userVote == null)
            {
                userVote = new ArticleVote(userId, articleId);
                userVote.Downvoted();
                _articleVoteRepository.Insert(userVote);
            }
            else
            {
                userVote.Downvoted();
                _articleVoteRepository.Update(userVote);
            }

            _articleRepository.Downvote(articleId, decrement);

            return RedirectToAction("ReadArticle", new { articleId = articleId });
        }

        [HttpPost]
        public async Task<IActionResult> WriteComment(AddCommentViewModel addCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(addCommentViewModel.UserId.ToString());
                var comment = new Comment(text: addCommentViewModel.Text, articleId: addCommentViewModel.ArticleId,
                    userId: addCommentViewModel.UserId, userName: user.UserName);
                _commentRepository.Insert(comment);
            }

            return RedirectToAction("ReadArticle", new {articleId = addCommentViewModel.ArticleId});
        }

        [Authorize(Roles = ConcatenatedRoles)]
        public async Task<IActionResult> WriteArticle()
        {
            ViewBag.AutorId = await GetActiveUserId();
            ViewBag.CategoryId = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = ConcatenatedRoles)]
        public async Task<IActionResult> WriteArticle(AddArticleViewModel addArticleViewModel)
        {
            if (ModelState.IsValid)
            {
                var article = new Article(addArticleViewModel.Title, addArticleViewModel.Text, addArticleViewModel.Summary,
                    addArticleViewModel.UserId, addArticleViewModel.CategoryId);
                _articleRepository.Insert(article);

                return RedirectToAction("ReadArticle", new { articleId = article.ArticleId});
            }

            ViewBag.AutorId = await GetActiveUserId();
            ViewBag.CategoryId = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name",
                addArticleViewModel.CategoryId);

            return View(addArticleViewModel);
        }

        [Authorize(Roles = ConcatenatedRoles)]
        public async Task<IActionResult> EditArticle(Guid articleId)
        {
            var article = _articleRepository.GetById(articleId);
            ViewBag.CategoryId = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name",
                article.CategoryId);

            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = ConcatenatedRoles)]
        public async Task<IActionResult> EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleRepository.Update(article);
                return RedirectToAction("ReadArticle", new { articleId = article.ArticleId });
            }

            ViewBag.CategoryId = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "Name",
                article.CategoryId);

            return View(article);
        }

        [Authorize(Roles = ConcatenatedRoles)]
        public IActionResult DeleteArticle(Guid articleid)
        {
            var article = _articleRepository.GetById(articleid);
            _articleRepository.Delete(articleid);
            return RedirectToAction("CategoryArticles", "Category", new { categoryId = article.CategoryId});
        }

        private async Task<Guid> GetActiveUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Guid.Parse(user.Id);
        }
    }
}
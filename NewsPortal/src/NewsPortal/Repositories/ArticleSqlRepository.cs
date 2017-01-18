using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Repositories
{
    public class ArticleSqlRepository : IArticleRepository
    {
        private readonly NewsPortalDbContext _dbContext;

        public ArticleSqlRepository(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Article>> GetAll()
        {
            return _dbContext.Articles.OrderByDescending(m => m.Date).ToListAsync();
        }

        public Article GetById(Guid articleId)
        {
            return _dbContext.Articles.Find(articleId);
        }

        public List<Comment> GetArticleComments(Guid articleId)
        {
            return GetById(articleId).Comments.OrderByDescending(m => m.Date).ToList();
        }

        public Task<ArticleVote> GetArticleVotes(Guid articleId, Guid userId)
        {
            return
                _dbContext.ArticleVotes.Where(m => (m.ArticleId == articleId) && (m.UserId == userId))
                    .FirstOrDefaultAsync();
        }

        public Task<List<Article>> GetAllWhere(Expression<Func<Article, bool>> predicate)
        {
            return _dbContext.Articles.Where(predicate).OrderByDescending(m => m.Date).ToListAsync();
        }

        public void Upvote(Guid articleId, bool increment)
        {
            var article = GetById(articleId);
            article.Upvote();

            if (increment)
            {
                article.Upvote();
            }

            Update(article);
        }

        public void Downvote(Guid articleId, bool decrement)
        {
            var article = GetById(articleId);
            article.DownVote();

            if (decrement)
            {
                article.DownVote();
            }

            Update(article);
        }

        public void Insert(Article entity)
        {
            _dbContext.Articles.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Insert(AddArticleViewModel entity)
        {
            Insert(
                new Article(entity.Title, entity.Text, entity.Summary, entity.UserId, entity.CategoryId)
            );
        }

        public void Update(Article entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid articleId)
        {
            _dbContext.Articles.Remove(GetById(articleId));
            _dbContext.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Interfaces;
using NewsPortal.Models;

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
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetAllWhere(Expression<Func<Article, bool>> predicate)
        {
            return _dbContext.Articles.Where(predicate).OrderByDescending(m => m.Date).ToListAsync();
        }

        public void Insert(Article entity)
        {
            _dbContext.Articles.Add(entity);
            _dbContext.SaveChanges();
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

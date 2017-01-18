using System;
using System.Data.Entity;
using System.Linq;
using NewsPortal.Interfaces;
using NewsPortal.Models;

namespace NewsPortal.Repositories
{
    public class ArticleVoteSqlRepository : IArticleVoteRepository
    {
        private readonly NewsPortalDbContext _dbContext;

        public ArticleVoteSqlRepository(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ArticleVote GetUsersVote(Guid articleId, Guid userId)
        {
            return _dbContext.ArticleVotes.FirstOrDefault(m => (m.ArticleId == articleId) && (m.UserId == userId));
        }

        public void Insert(ArticleVote entity)
        {
            _dbContext.ArticleVotes.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(ArticleVote entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid articleVoteId)
        {
            _dbContext.ArticleVotes.Remove(_dbContext.ArticleVotes.Find(articleVoteId));
            _dbContext.SaveChanges();
        }
    }
}
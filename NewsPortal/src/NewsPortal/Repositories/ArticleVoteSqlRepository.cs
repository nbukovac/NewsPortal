using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

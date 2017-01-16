using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Interfaces;
using NewsPortal.Models;

namespace NewsPortal.Repositories
{
    public class ArticleSqlRepository : IArticleRepository
    {
        public Task<List<Article>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Article> GetById(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetArticleComments(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleVote> GetArticleVotes(Guid articleId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetAllWhere(Expression<Func<Article, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Insert(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Article entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid articleId)
        {
            throw new NotImplementedException();
        }
    }
}

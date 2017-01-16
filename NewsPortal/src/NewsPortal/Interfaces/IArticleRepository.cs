using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Models;

namespace NewsPortal.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAll();

        Task<Article> GetById(Guid articleId);

        Task<List<Comment>> GetArticleComments(Guid articleId);

        Task<ArticleVote> GetArticleVotes(Guid articleId, Guid userId);

        Task<List<Article>> GetAllWhere(Expression<Func<Article, bool>> predicate);

        void Insert(Article entity);

        void Update(Article entity);

        void Delete(Guid articleId);
    }
}

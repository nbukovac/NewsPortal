using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAll();

        Article GetById(Guid articleId);

        List<Comment> GetArticleComments(Guid articleId);

        Task<ArticleVote> GetArticleVotes(Guid articleId, Guid userId);

        Task<List<Article>> GetAllWhere(Expression<Func<Article, bool>> predicate);

        void Upvote(Guid articleId, bool increment);

        void Downvote(Guid articleId, bool decrement);

        void Insert(Article entity);

        void Insert(AddArticleViewModel entity);

        void Update(Article entity);

        void Delete(Guid articleId);
    }
}
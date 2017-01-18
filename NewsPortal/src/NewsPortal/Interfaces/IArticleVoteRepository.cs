using System;
using NewsPortal.Models;

namespace NewsPortal.Interfaces
{
    public interface IArticleVoteRepository
    {
        ArticleVote GetUsersVote(Guid articleId, Guid userId);

        void Insert(ArticleVote entity);

        void Update(ArticleVote entity);

        void Delete(Guid articleVoteId);
    }
}
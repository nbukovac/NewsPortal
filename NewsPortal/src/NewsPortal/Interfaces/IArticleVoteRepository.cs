using System;
using NewsPortal.Models;

namespace NewsPortal.Interfaces
{
    public interface IArticleVoteRepository
    {
        void Insert(ArticleVote entity);

        void Update(ArticleVote entity);

        void Delete(Guid articleVoteId);
    }
}
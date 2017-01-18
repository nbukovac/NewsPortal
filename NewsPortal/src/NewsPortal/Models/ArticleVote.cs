using System;

namespace NewsPortal.Models
{
    public class ArticleVote
    {
        public ArticleVote()
        {
        }

        public ArticleVote(Guid userId, Guid articleId)
        {
            ArticleVoteId = Guid.NewGuid();
            UserId = userId;
            ArticleId = articleId;
        }

        public void Upvoted()
        {
            UpVote = true;
            DownVote = false;
        }

        public void Downvoted()
        {
            DownVote = true;
            UpVote = false;
        }

        public Guid ArticleVoteId { get; set; }
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
        public bool DownVote { get; set; }
        public bool UpVote { get; set; }

        public virtual Article Article { get; set; }
    }
}
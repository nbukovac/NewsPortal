using System;

namespace NewsPortal.Models
{
    public class ArticleVote
    {
        public ArticleVote()
        {
        }

        public ArticleVote(Guid userId, Guid articleId, bool down, bool up)
        {
            ArticleVoteId = Guid.NewGuid();
            UserId = userId;
            ArticleId = articleId;
            DownVote = down;
            UpVote = up;
        }

        public Guid ArticleVoteId { get; set; }
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
        public bool DownVote { get; set; }
        public bool UpVote { get; set; }

        public virtual Article Article { get; set; }
    }
}
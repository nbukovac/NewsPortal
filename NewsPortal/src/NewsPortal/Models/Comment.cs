using System;
using System.ServiceModel.Security;

namespace NewsPortal.Models
{
    public class Comment
    {
        public Comment(Guid userId, Guid articleId, string text, string userName)
        {
            CommentId = Guid.NewGuid();
            UserId = userId;
            Date = DateTime.Now;
            Text = text;
            ArticleId = articleId;
            UserName = userName;
        }

        public string UserName { get; set; }

        public Comment()
        {
        }

        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public Guid ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
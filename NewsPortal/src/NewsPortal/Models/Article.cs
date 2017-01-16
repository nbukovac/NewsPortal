using System;
using System.Collections.Generic;

namespace NewsPortal.Models
{
    public class Article
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public int Votes { get; set; }
        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
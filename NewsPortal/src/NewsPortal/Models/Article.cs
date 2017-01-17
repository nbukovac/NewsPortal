using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class Article
    {
        [Required]
        public Guid ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public int Votes { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<ArticleVote> VotingList { get; set; }


        public Article()
        {
            
        }

        public Article(string title, string text, string summary, Guid userId, Guid categoryId)
        {
            ArticleId = Guid.NewGuid();
            Title = title;
            Text = text;
            Summary = summary;
            UserId = userId;
            CategoryId = categoryId;
            Votes = 0;
            Date = DateTime.Now;
        }

        public int GetTrendingScore()
        {
            return Votes + 2*Comments.Count;
        }

        public void Upvote()
        {
            Votes++;
        }

        public void DownVote()
        {
            Votes--;
        }
    }
}
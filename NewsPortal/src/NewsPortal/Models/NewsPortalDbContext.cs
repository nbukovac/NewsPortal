using System.Data.Entity;

namespace NewsPortal.Models
{
    public class NewsPortalDbContext : DbContext
    {
        public NewsPortalDbContext(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<ArticleVote> ArticleVotes { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Article
            modelBuilder.Entity<Article>().HasKey(t => t.ArticleId);
            modelBuilder.Entity<Article>().Property(t => t.Date).IsRequired();
            modelBuilder.Entity<Article>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<Article>().Property(t => t.Summary).IsRequired();
            modelBuilder.Entity<Article>().Property(t => t.Votes).IsRequired();
            modelBuilder.Entity<Article>().Property(t => t.CategoryId).IsRequired();
            modelBuilder.Entity<Article>().Property(t => t.UserId).IsRequired();
            modelBuilder.Entity<Article>().Property(t => t.Title).IsRequired();

            modelBuilder.Entity<Article>().HasRequired(t => t.Category);
            modelBuilder.Entity<Article>().HasMany(t => t.Comments);

            //Comments
            modelBuilder.Entity<Comment>().HasKey(t => t.CommentId);
            modelBuilder.Entity<Comment>().Property(t => t.ArticleId).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.Date).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.Text).IsRequired();
            modelBuilder.Entity<Comment>().Property(t => t.UserId).IsRequired();

            modelBuilder.Entity<Comment>().HasRequired(t => t.Article);

            //Categories
            modelBuilder.Entity<Category>().HasKey(t => t.CategoryId);
            modelBuilder.Entity<Category>().Property(t => t.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(t => t.Description).IsRequired();

            modelBuilder.Entity<Category>().HasMany(t => t.Articles);

            //ArticleVotes
            modelBuilder.Entity<ArticleVote>().HasKey(t => t.ArticleVoteId);
            modelBuilder.Entity<ArticleVote>().Property(t => t.ArticleId).IsRequired();
            modelBuilder.Entity<ArticleVote>().Property(t => t.DownVote).IsRequired();
            modelBuilder.Entity<ArticleVote>().Property(t => t.UpVote).IsRequired();
            modelBuilder.Entity<ArticleVote>().Property(t => t.UserId).IsRequired();

            modelBuilder.Entity<ArticleVote>().HasRequired(t => t.Article);
        }
    }
}
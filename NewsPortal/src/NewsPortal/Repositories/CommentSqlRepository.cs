using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Interfaces;
using NewsPortal.Models;

namespace NewsPortal.Repositories
{
    public class CommentSqlRepository : ICommentRepository
    {
        private readonly NewsPortalDbContext _dbContext;

        public CommentSqlRepository(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Comment>> GetAll()
        {
            return _dbContext.Comments.OrderBy(m => m.Date).ToListAsync();
        }

        public Comment GetById(Guid commentId)
        {
            return _dbContext.Comments.Find(commentId);
        }

        public Task<List<Comment>> GetAllWhere(Expression<Func<Comment, bool>> predicate)
        {
            return _dbContext.Comments.Where(predicate).OrderBy(m => m.Date).ToListAsync();
        }

        public void Insert(Comment entity)
        {
            _dbContext.Comments.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Comment entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid commentId)
        {
            _dbContext.Comments.Remove(GetById(commentId));
            _dbContext.SaveChanges();
        }
    }
}

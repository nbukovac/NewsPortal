using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Interfaces;
using NewsPortal.Models;

namespace NewsPortal.Repositories
{
    public class CommentSqlRepository : ICommentRepository
    {
        public Task<List<Comment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetById(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllWhere(Expression<Func<Comment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Insert(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}

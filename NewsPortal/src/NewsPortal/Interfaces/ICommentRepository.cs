using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll();

        Comment GetById(Guid commentId);

        Task<List<Comment>> GetAllWhere(Expression<Func<Comment, bool>> predicate);

        void Insert(Comment entity);

        void Update(Comment entity);

        void Delete(Guid commentId);
    }
}

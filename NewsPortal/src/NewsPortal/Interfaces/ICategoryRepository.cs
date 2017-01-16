using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Models;

namespace NewsPortal.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();

        Task<Category> GetById(Guid categoryId);

        Task<List<Article>> GetCategoryArticles(Guid categoryId);

        Task<List<Category>> GetAllWhere(Expression<Func<Category, bool>> predicate);

        void Insert(Category entity);

        void Update(Category entity);

        void Delete(Guid categoryId);
    }
}
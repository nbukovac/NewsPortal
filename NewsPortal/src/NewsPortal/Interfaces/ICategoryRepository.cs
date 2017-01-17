using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();

        Category GetById(Guid categoryId);

        List<Article> GetCategoryArticles(Guid categoryId);

        Task<List<Article>> GetTrendingArticles(Guid categoryId, int n);

        Task<List<Category>> GetAllWhere(Expression<Func<Category, bool>> predicate);

        Task<List<Article>> GetNewestArticles(Guid categoryId, int n);

        void Insert(Category entity);

        void Insert(AddCategoryViewModel entity);

        void Update(Category entity);

        void Delete(Guid categoryId);
    }
}
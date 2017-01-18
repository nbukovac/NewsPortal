using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NewsPortal.Interfaces;
using NewsPortal.Models;
using NewsPortal.Models.ViewModels;

namespace NewsPortal.Repositories
{
    public class CategorySqlRepository : ICategoryRepository
    {

        private readonly NewsPortalDbContext _dbContext;

        public CategorySqlRepository(NewsPortalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Category>> GetAll()
        {
            return _dbContext.Categories.OrderBy(m => m.Name).ToListAsync();
        }

        public Category GetById(Guid categoryId)
        {
            return _dbContext.Categories.Find(categoryId);
        }

        public List<Article> GetCategoryArticles(Guid categoryId)
        {
            return GetById(categoryId).Articles.OrderBy(m => m.Date).ToList();
        }

        public Task<List<Article>> GetTrendingArticles(Guid categoryId, int n)
        {
            return
                _dbContext.Articles.Where(m => m.CategoryId == categoryId)
                    .OrderByDescending(m => m.Date)
                    .Take(n)
                    .OrderByDescending(m => m.Votes + m.Comments.Count*2)
                    .ToListAsync();
        }

        public Task<List<Category>> GetAllWhere(Expression<Func<Category, bool>> predicate)
        {
            return _dbContext.Categories.Where(predicate).ToListAsync();
        }

        public Task<List<Article>> GetNewestArticles(Guid categoryId, int n)
        {
            return
                _dbContext.Articles.Where(m => m.CategoryId == categoryId)
                    .OrderByDescending(m => m.Date)
                    .Take(n)
                    .ToListAsync();
        }

        public void Insert(Category entity)
        {
            _dbContext.Categories.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Insert(AddCategoryViewModel entity)
        {
            _dbContext.Categories.Add(new Category(entity.Name, entity.Description));
            _dbContext.SaveChanges();
        }

        public void Update(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid categoryId)
        {
            _dbContext.Categories.Remove(GetById(categoryId));
            _dbContext.SaveChanges();
        }
    }
}

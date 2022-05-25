using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Category entity)
        {           
            await _db.Categories.AddAsync(entity);
            return await Save();
        }

        public async Task<int> CheckValid(Category entity)
        {
            var sameSubCategory = await _db.Categories.Where(c => c.Title.ToLower() == entity.Title.ToLower())
                                                      .ToListAsync(); //
            return sameSubCategory.Count;
        }

        public Task<bool> Delete(Category entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<Category>> FindAll()
        {
            var listCategories = await _db.Categories.Include(c => c.CategoryParent)
                                                     .ToListAsync(); //
            return listCategories;
        }
        public async Task<ICollection<Category>> GetCategory()
        {
            var listCategories = await _db.Categories.Include(c => c.CategoryParent)
                                                     .Where(c=>c.ParentID ==null)
                                                     .ToListAsync(); //
            return listCategories;
        }

        public async Task<Category> FindById(int id)
        {
            var listCategories = await _db.Categories.Where(p => p.Id == id)
                                                     .FirstOrDefaultAsync(); //
            return listCategories;
        }

        public Task<bool> isExists(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Category entity)
        {
            _db.Categories.Update(entity);
            return await Save();
        }
    }
}

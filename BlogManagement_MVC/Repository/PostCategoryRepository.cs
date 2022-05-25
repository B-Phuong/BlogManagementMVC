using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Repository
{
    public class PostCategoryRepository : IPostCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public PostCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(PostCategory entity)
        {
            await _db.PostCategories.AddAsync(entity);
            return await Save();
        }

        public Task<bool> Delete(PostCategory entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<PostCategory>> FindAll()
        {
            var posts = await _db.PostCategories.Include(pc => pc.Post)
                                                .Include(pc=>pc.Category)
                                                .Where(pc=> pc.Post.Published == 1 && pc.Post.IsDeleted == false )
                                                .ToListAsync();
            return posts;
        }

        public async Task<ICollection<PostCategory>> FindByCategory(int id)
        {
            // var post = await _db.Posts.FromSql
            var posts = await _db.PostCategories.Include(pc=>pc.Post)
                                                .Include(p=>p.Category)
                                                .OrderBy(p => p.Post.CreatedAt)
                                                .ToListAsync();
            return posts.Where(pc => pc.CategoryId == id && pc.Post.IsDeleted == false && pc.Post.Published == 1).ToList();
        }

        public async Task<ICollection<PostCategory>> GetPostCategoryByPaging(int currentPage, int pageSize = 4)
        {
            var posts = await _db.PostCategories.Include(pc => pc.Post)
                                                .Include(p => p.Category)
                                                .Where(pc => pc.Post.Published == 1 && pc.Post.IsDeleted == false)
                                                .OrderBy(p => p.Post.CreatedAt)
                                                .Skip((currentPage - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
            return posts;
        }

        public async Task<ICollection<PostCategory>> GetPostByCategoryAfterPaging(int id, int currentPage, int pageSize = 4)
        {
            var posts = await _db.PostCategories.Include(pc => pc.Post)
                                                .Include(p => p.Category)
                                                .Where(pc => pc.Post.Published == 1 && pc.Post.IsDeleted == false && pc.CategoryId == id)
                                                .OrderBy(p => p.Post.CreatedAt)
                                                .Skip((currentPage - 1) * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
            return posts;
        }

        public async Task<PostCategory> FindById(int id)
        {
            throw new System.NotImplementedException();
        }
        public async Task<ICollection<PostCategory>> FindCategoryByPost(int id)
        {
            var post = await _db.PostCategories.Include(pc => pc.Category)
                                               .Where(pc => pc.PostId == id && pc.Post.Published == 1 && pc.Post.IsDeleted == false)
                                               .ToListAsync();
            return post;
        }
        public Task<bool> isExists(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveAll(ICollection<PostCategory> listCategoryOfPost)
        {
            _db.PostCategories.RemoveRange(listCategoryOfPost);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(PostCategory entity)
        {
            _db.PostCategories.Update(entity);
            return await Save();
        }
    }
}

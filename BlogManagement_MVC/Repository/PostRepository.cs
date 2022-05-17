using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Post entity)
        {
            await _db.Posts.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Post entity)
        {         
            entity.IsDeleted = true;
            _db.Posts.Update(entity);
            return await Save();
        }
        public async Task<bool> Private(Post entity)
        {
            entity.Published = 0;
            _db.Posts.Update(entity);
            return await Save();
        }
        public async Task<bool> Publish(Post entity)
        {
            entity.Published =1;
            _db.Posts.Update(entity);
            return await Save();
        }

        public async Task<ICollection<Post>> FindAll()
        {
            var posts = await _db.Posts.Include(p=>p.Author)
                                       .Where(p => p.Published == 1 && p.IsDeleted == false)
                                       .ToListAsync();
            return posts;
        }

        public async Task<ICollection<Post>> GetPostByPaging(int currentPage, int pageSize = 4)
        {
            var posts = await _db.Posts.Include(p => p.Author)
                                       .Where(p => p.Published == 1 && p.IsDeleted == false)
                                       .OrderBy(p => p.CreatedAt)  
                                       .Skip((currentPage - 1) * pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();
            return posts;
        }

        public async Task<Post> FindById(int id)
        {
            var post = await _db.Posts.Include(p => p.Author)
                                      .Include(p => p.PostComments)
                                      .Where(p => p.Id == id && p.IsDeleted == false)
                                      .FirstOrDefaultAsync();
            return post;
        }

        public async Task<bool> isExists(string id)
        {
           // var exists = await _db.Posts.AnyAsync(q => q.Id == id);
           // return exists;
            throw new System.NotImplementedException();
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Post entity)
        {
            _db.Posts.Update(entity);
            return await Save();
        }

      
    }
}

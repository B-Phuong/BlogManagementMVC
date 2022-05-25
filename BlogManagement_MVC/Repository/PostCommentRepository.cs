using BlogManagement_MVC.Contracts;
using BlogManagement_MVC.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Repository
{
    public class PostCommentRepository : IPostCommentRepository
    {
        private readonly ApplicationDbContext _db;

        public PostCommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(PostComment entity)
        {
            await _db.PostComments.AddAsync(entity);
            return await Save();
        }

        public Task<bool> Delete(PostComment entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ICollection<PostComment>> FindAll()
        {
            var postComments = await _db.PostComments.Include(pc => pc.AuthorComment)
                                                     .Include(pc => pc.Post)
                                                     .ToListAsync();
            return postComments.ToList();
        }

        public async Task<PostComment> FindById(int id)
        {
            // var post = await _db.Posts.FromSql
            var postComments = await _db.PostComments.Include(pc => pc.AuthorComment)
                                                     .Include(pc => pc.Post)
                                                     .Where(pc=>pc.Id == id)
                                                     .FirstOrDefaultAsync();
            return postComments;
        }

        public async Task<ICollection<PostComment>> FindByPost(int id)
        {
            var postComments = await _db.PostComments.Include(pc=>pc.AuthorComment)
                                                     .Include(pc=>pc.Post)
                                                     .Where(pc => pc.PostId == id && pc.Published == 1)
                                                     .ToListAsync(); //&& pc.Published == 1
            return postComments;//.ToList();
        }
        public async Task<bool> HiddenComment(PostComment entity)
        {
            entity.Published = 0;
            if (entity.ParentID == null)
            {
                
                var listSubComment = await _db.PostComments.Where(pc => pc.ParentID == entity.Id)
                                                           .ToListAsync();
                foreach (var item in listSubComment)
                {
                    item.Published = 0;
                    _db.PostComments.Update(item);
                }
            }
            
           _db.PostComments.Update(entity);
            return await Save();
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

        public async Task<bool> Update(PostComment entity)
        {
            _db.PostComments.Update(entity);
            return await Save();
        }
    }
}

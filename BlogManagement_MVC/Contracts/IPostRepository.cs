using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Contracts
{
    public interface IPostRepository: IRepositoryBase<Post>
    {
        Task<bool> Private(Post entity);
        Task<ICollection<Post>> FindPosts();
        Task<bool> Publish(Post entity);
        Task<ICollection<Post>> GetPostByPaging(int currentPage, int pageSize = 4);
       
    }
}

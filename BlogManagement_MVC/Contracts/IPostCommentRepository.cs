using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Contracts
{
    public interface IPostCommentRepository : IRepositoryBase<PostComment>
    {
        Task<ICollection<PostComment>> FindByPost(int id);
        Task<bool> HiddenComment(PostComment entity);
    }
}

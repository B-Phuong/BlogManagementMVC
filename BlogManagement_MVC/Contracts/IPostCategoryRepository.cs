using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Contracts
{
    public interface IPostCategoryRepository:IRepositoryBase<PostCategory>
    {
        Task<ICollection<PostCategory>> FindByCategory(int id);
        Task<ICollection<PostCategory>> FindCategoryByPost(int id);
        Task<ICollection<PostCategory>> GetPostCategoryByPaging(int currentPage, int pageSize = 4);
        Task<ICollection<PostCategory>> GetPostByCategoryAfterPaging(int id, int currentPage, int pageSize = 4);
        Task<bool> RemoveAll(ICollection<PostCategory> listCategoryOfPost);

    }
}

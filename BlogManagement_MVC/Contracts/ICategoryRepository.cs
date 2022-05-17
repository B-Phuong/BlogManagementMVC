using BlogManagement_MVC.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement_MVC.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<ICollection<Category>> GetCategory();
    }
}

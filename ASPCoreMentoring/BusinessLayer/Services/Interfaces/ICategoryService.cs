using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using BusinessLayer.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryDTO>> GetPagedCategory(int pageSize = 0, int page = 1);

        Task<IEnumerable<CategoryDTO>> GetAllCategories();
    }
}

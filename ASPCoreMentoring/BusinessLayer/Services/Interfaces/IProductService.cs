using BusinessLayer.Pagination;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;

namespace BusinessLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductDTO>> GetPagedProduct(int pageSize = 0, int page = 1);

        Task CreateProductAsync(ProductDTO product);

        Task UpdateProductAsync(ProductDTO product);

        Task<ProductDTO> GetByIdAsync(int productId);
    }
}

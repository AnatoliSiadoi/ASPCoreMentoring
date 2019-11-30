using BusinessLayer.Pagination;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using System.Collections.Generic;

namespace BusinessLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductDTO>> GetPagedProduct(int pageSize = 0, int page = 1);

        Task<IEnumerable<ProductDTO>> GetAllProducts(bool includeCategory, bool includeSupplier);

        Task CreateProductAsync(ProductDTO product);

        Task DeleteProductAsync(ProductDTO productId);

        Task UpdateProductAsync(ProductDTO product);

        Task<ProductDTO> GetByIdAsync(int productId);
    }
}

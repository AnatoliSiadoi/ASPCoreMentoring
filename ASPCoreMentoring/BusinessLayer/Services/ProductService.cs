using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using BusinessLayer.Pagination;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;

        public ProductService(IRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task CreateProductAsync(ProductDTO product)
        {
            var productEntity = ConvertProductDTOToEntity(product);
            await _productRepository.AddAsync(productEntity);
        }

        public async Task UpdateProductAsync(ProductDTO product)
        {
            var productEntity = ConvertProductDTOToEntity(product);
            await _productRepository.UpdateAsync(productEntity);
        }

        public async Task DeleteProductAsync(ProductDTO product)
        {
            var productEntity = ConvertProductDTOToEntity(product);
            await _productRepository.DeleteAsync(productEntity);
        }

        public async Task<ProductDTO> GetByIdAsync(int productId)
        {
            var productEntity = await _productRepository.GetByIdAsync(productId);

            return ConvertProductEntityToDTO(productEntity);
        }

        public async Task<PagedResult<ProductDTO>> GetPagedProduct(int pageSize = 0, int page = 1)
        {
            PagedResult<ProductEntity> result = null;

            var allProductSource = _productRepository.GetAllQueryable().Include(p => p.Category).Include(p => p.Supplier);
            if (allProductSource.Any())
            {
                result = await allProductSource.GetPagedAsync(pageSize, page);
            }

            return ConvertToPagedProductDTO(result);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts(bool includeCategory, bool includeSupplier)
        {
            IEnumerable<ProductDTO> result = null;

            var allProductList = await IncludeToProduct(includeCategory, includeSupplier); 
            if (allProductList.Any())
            {
                result = allProductList.Select(ConvertProductEntityToDTO);
            }

            return result;
        }

        private async Task<List<ProductEntity>> IncludeToProduct(bool includeCategory, bool includeSupplier)
        {
            List<ProductEntity> allProductList = null;
            if (includeCategory == false && includeSupplier == false)
            {
                allProductList = await _productRepository.GetAllQueryable().ToListAsync();
                return allProductList;
            }
            else if (includeCategory == true && includeSupplier == true)
            {
                allProductList = await _productRepository.GetAllQueryable().Include(p => p.Category).Include(p => p.Supplier).ToListAsync();
                return allProductList;
            }
            else if (includeCategory == true && includeSupplier == false)
            {
                allProductList = await _productRepository.GetAllQueryable().Include(p => p.Category).ToListAsync();
                return allProductList;
            }
            else if (includeCategory == false && includeSupplier == true)
            {
                allProductList = await _productRepository.GetAllQueryable().Include(p => p.Supplier).ToListAsync();
                return allProductList;
            }

            return allProductList;
        }

        private PagedResult<ProductDTO> ConvertToPagedProductDTO(PagedResult<ProductEntity> inputEntity)
        {
            if (inputEntity == null)
                return null;

            return new PagedResult<ProductDTO>
            {
                CurrentPage = inputEntity.CurrentPage,
                PageCount = inputEntity.PageCount,
                PageSize = inputEntity.PageSize,
                RowCount = inputEntity.RowCount,
                Results = inputEntity.Results?.Select(ConvertProductEntityToDTO).ToList()
            };
        }

        private ProductDTO ConvertProductEntityToDTO(ProductEntity inputEntity)
        {
            if (inputEntity == null)
                return null;

            return new ProductDTO
            {
                Id = inputEntity.Id,
                ProductName = inputEntity.ProductName,
                CategoryID = inputEntity.CategoryID,
                QuantityPerUnit = inputEntity.QuantityPerUnit,
                SupplierID = inputEntity.SupplierID,
                UnitPrice = inputEntity.UnitPrice,
                UnitsInStock = inputEntity.UnitsInStock,
                UnitsOnOrder = inputEntity.UnitsOnOrder,
                ReorderLevel = inputEntity.ReorderLevel,
                Discontinued = inputEntity.Discontinued,
                Supplier = ConvertSupplierEntityToDTO(inputEntity.Supplier),
                Category = ConvertCategoryEntityToDTO(inputEntity.Category)
            };
        }

        private ProductEntity ConvertProductDTOToEntity(ProductDTO inputDTO)
        {
            if (inputDTO == null)
                return null;

            return new ProductEntity
            {
                Id = inputDTO.Id,
                ProductName = inputDTO.ProductName,
                CategoryID = inputDTO.CategoryID,
                QuantityPerUnit = inputDTO.QuantityPerUnit,
                SupplierID = inputDTO.SupplierID,
                UnitPrice = inputDTO.UnitPrice,
                UnitsInStock = inputDTO.UnitsInStock,
                UnitsOnOrder = inputDTO.UnitsOnOrder,
                ReorderLevel = inputDTO.ReorderLevel,
                Discontinued = inputDTO.Discontinued
            };
        }

        private CategoryDTO ConvertCategoryEntityToDTO(CategoryEntity inputEntity)
        {
            if (inputEntity == null)
                return null;

            return new CategoryDTO
            {
                Id = inputEntity.Id,
                CategoryName = inputEntity.CategoryName,
                Description = inputEntity.Description
            };
        }

        private SupplierDTO ConvertSupplierEntityToDTO(SupplierEntity inputEntity)
        {
            if (inputEntity == null)
                return null;

            return new SupplierDTO()
            {
                Id = inputEntity.Id,
                CompanyName = inputEntity.CompanyName
            };
        }
    }
}

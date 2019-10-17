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
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<CategoryEntity> _categoryRepository;

        public CategoryService(IRepository<CategoryEntity> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResult<CategoryDTO>> GetPagedCategory(int pageSize = 0, int page = 1)
        {
            PagedResult<CategoryEntity> result = null;

            var allCategorySource = _categoryRepository.GetAllQueryable();
            if (allCategorySource.Any())
            {
                result = await allCategorySource.GetPagedAsync(pageSize, page);
            }

            return ConvertToPagedCategoryDTO(result);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            IEnumerable<CategoryDTO> result = null;
            var allCategoryList = await _categoryRepository.GetAllQueryable().ToListAsync();
            if (allCategoryList.Any())
            {
                result = allCategoryList.Select(ConvertCategoryEntityToDTO);
            }

            return result;
        }

        private PagedResult<CategoryDTO> ConvertToPagedCategoryDTO(PagedResult<CategoryEntity> inputEntity)
        {
            if (inputEntity == null)
                return null;

            return new PagedResult<CategoryDTO>
            {
                CurrentPage = inputEntity.CurrentPage,
                PageCount = inputEntity.PageCount,
                PageSize = inputEntity.PageSize,
                RowCount = inputEntity.RowCount,
                Results = inputEntity.Results?.Select(ConvertCategoryEntityToDTO).ToList()
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
                Description = inputEntity.Description,
            };
        }
    }
}

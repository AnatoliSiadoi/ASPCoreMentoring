using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MVCPresentationLayer.Configuration;
using MVCPresentationLayer.Models.Category;

namespace MVCPresentationLayer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IOptions<PaginationSection> _config;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IOptions<PaginationSection> config, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _config = config;
            _logger = logger;
        }

        public async Task<ActionResult> Index(int? page)
        {
            var pagedCategories = await _categoryService.GetPagedCategory(_config.Value.CountItemOnPage, page ?? _config.Value.PageNumber);
            var viewModel = pagedCategories?.Results?.Select(ConvertToCategoryViewModel);

            if (pagedCategories != null)
            {
                var pagingInfo = new Pagination.PagingInfo()
                {
                    CurrentPage = pagedCategories.CurrentPage,
                    TotalItems = pagedCategories.RowCount,
                    ItemsPerPage = pagedCategories.PageSize
                };
                ViewBag.Paging = pagingInfo;
            }

            return View("Index", viewModel);
        }

        private CategoryViewModel ConvertToCategoryViewModel(CategoryDTO inputDTO)
        {
            if (inputDTO == null)
                return null;

            return new CategoryViewModel
            {
                Id = inputDTO.Id,
                CategoryName = inputDTO.CategoryName,
                Description = inputDTO.Description
            };
        }
    }
}
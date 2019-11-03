using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult> Edit(int id)
        {
            var categoryDTO = await _categoryService.GetCategoryById(id);
            var viewModel = ConvertToCategoryViewModel(categoryDTO);

            if (viewModel == null)
                return NotFound();

            return View("Edit", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Picture(int id)
        {
            var picture = await _categoryService.GetPictureById(id);

            if (picture == null)
                return NotFound();

            return File(picture, "image/bmp");
        }

        [HttpPost]
        public async Task<IActionResult> Picture(CategoryPictureViewModel model)
        {
            var categoryDTO = await _categoryService.GetCategoryById(model.Id);
            var category = ConvertToCategoryViewModel(categoryDTO);
            if (!ModelState.IsValid)
                return View("Edit", category);

            var picture = await ConvertToByte(model.Picture);

            await _categoryService.UpdatePictureById(model.Id, picture);

            return View("Edit", category);
        }

        private async Task<byte[]> ConvertToByte(IFormFile formFile)
        {
            if(formFile == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
        }

        private CategoryViewModel ConvertToCategoryViewModel(CategoryDTO inputDTO)
        {
            if (inputDTO == null)
                return null;

            return new CategoryViewModel
            {
                Id = inputDTO.Id,
                CategoryName = inputDTO.CategoryName,
                Description = inputDTO.Description,
                PictureLink = inputDTO.PictureLink
            };
        }
    }
}
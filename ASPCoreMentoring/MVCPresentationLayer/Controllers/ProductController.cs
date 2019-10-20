using System;
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
using MVCPresentationLayer.Models.Product;
using MVCPresentationLayer.Models.Supplier;

namespace MVCPresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ICategoryService _categoryService;
        private readonly IOptions<PaginationSection> _config;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ISupplierService supplierService, ICategoryService categoryService, IOptions<PaginationSection> config, ILogger<ProductController> logger)
        {
            _productService = productService;
            _supplierService = supplierService;
            _categoryService = categoryService;
            _config = config;
            _logger = logger;
        }
        public async Task<ActionResult> Index(int? page)
        {
            var pagedProducts = await _productService.GetPagedProduct(_config.Value.CountItemOnPage, page ?? _config.Value.PageNumber);
            var viewModel = pagedProducts?.Results?.Select(ConvertProductDTOToViewModel);

            if (pagedProducts != null)
            {
                var pagingInfo = new Pagination.PagingInfo()
                {
                    CurrentPage = pagedProducts.CurrentPage,
                    TotalItems = pagedProducts.RowCount,
                    ItemsPerPage = pagedProducts.PageSize
                };
                ViewBag.Paging = pagingInfo;
            }

            return View("Index", viewModel);
        }

        public async Task<ActionResult> Create()
        {
            var viewModel = new ProductViewModelCreate();
            var allCategory = await _categoryService.GetAllCategories();
            viewModel.CategoryList = allCategory.Select(ConvertCategoryDTOToViewModel);
            var allSuppliers = await _supplierService.GetAllSuppliers();
            viewModel.SupplierList = allSuppliers.Select(ConvertSuppliersDTOToViewModel);

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModelCreate product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Create", product);
                }

                var productDTO = ConvertCreateViewModelToDTO(product);
                await _productService.CreateProductAsync(productDTO);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong during the creation new product!");
                ModelState.AddModelError(string.Empty, "Something went wrong during the creation new product!");

                return View("Create", product);
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var productDTO = await _productService.GetByIdAsync(id);
            var viewModel = await ConvertProductDTOToViewModelUpdate(productDTO);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View("Edit", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProductViewModelUpdate product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", product);
                }

                var productDTO = ConvertUpdateViewModelToDTO(product);
                await _productService.UpdateProductAsync(productDTO);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong during the update product!");
                ModelState.AddModelError(string.Empty, "Something went wrong during the update product!");

                return View("Edit", product);
            }
        }

        private ProductDTO ConvertUpdateViewModelToDTO(ProductViewModelUpdate inputViewModel)
        {
            if (inputViewModel == null)
                return null;

            return new ProductDTO
            {
                Id = inputViewModel.Id,
                ProductName = inputViewModel.ProductName,
                QuantityPerUnit = inputViewModel.QuantityPerUnit,
                ReorderLevel = inputViewModel.ReorderLevel,
                UnitPrice = inputViewModel.UnitPrice,
                UnitsInStock = inputViewModel.UnitsInStock,
                UnitsOnOrder = inputViewModel.UnitsOnOrder,
                Discontinued = inputViewModel.Discontinued,
                SupplierID = inputViewModel.SupplierId,
                CategoryID = inputViewModel.CategoryId,
            };
        }

        private ProductDTO ConvertCreateViewModelToDTO(ProductViewModelCreate inputViewModel)
        {
            if (inputViewModel == null)
                return null;

            return new ProductDTO
            {
                ProductName = inputViewModel.ProductName,
                QuantityPerUnit = inputViewModel.QuantityPerUnit,
                ReorderLevel = inputViewModel.ReorderLevel,
                UnitPrice = inputViewModel.UnitPrice,
                UnitsInStock = inputViewModel.UnitsInStock,
                UnitsOnOrder = inputViewModel.UnitsOnOrder,
                Discontinued = inputViewModel.Discontinued,
                SupplierID = inputViewModel.SupplierId,
                CategoryID = inputViewModel.CategoryId,
            };
        }

        private ProductViewModel ConvertProductDTOToViewModel(ProductDTO inputDTO)
        {
            if (inputDTO == null)
                return null;

            return new ProductViewModel
            {
                Id = inputDTO.Id,
                ProductName = inputDTO.ProductName,
                QuantityPerUnit = inputDTO.QuantityPerUnit,
                UnitPrice = inputDTO.UnitPrice,
                UnitsInStock = inputDTO.UnitsInStock,
                UnitsOnOrder = inputDTO.UnitsOnOrder,
                ReorderLevel = inputDTO.ReorderLevel,
                Discontinued = inputDTO.Discontinued,
                SupplierName = inputDTO.Supplier?.CompanyName,
                CategoryName = inputDTO.Category?.CategoryName
            };
        }

        private async Task<ProductViewModelUpdate> ConvertProductDTOToViewModelUpdate(ProductDTO inputDTO)
        {
            if (inputDTO == null)
                return null;

            var result = new ProductViewModelUpdate
            {
                Id = inputDTO.Id,
                ProductName = inputDTO.ProductName,
                QuantityPerUnit = inputDTO.QuantityPerUnit,
                UnitPrice = inputDTO.UnitPrice,
                UnitsInStock = inputDTO.UnitsInStock,
                UnitsOnOrder = inputDTO.UnitsOnOrder,
                ReorderLevel = inputDTO.ReorderLevel,
                Discontinued = inputDTO.Discontinued,
                CategoryId = inputDTO.CategoryID,
                SupplierId = inputDTO.SupplierID
            };
            var allCategory = await _categoryService.GetAllCategories();
            result.CategoryList = allCategory.Select(ConvertCategoryDTOToViewModel);
            var allSuppliers = await _supplierService.GetAllSuppliers();
            result.SupplierList = allSuppliers.Select(ConvertSuppliersDTOToViewModel);

            return result;
        }

        private CategoryViewModel ConvertCategoryDTOToViewModel(CategoryDTO inputDTO)
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

        private SupplierViewModel ConvertSuppliersDTOToViewModel(SupplierDTO inputDTO)
        {
            if (inputDTO == null)
                return null;

            return new SupplierViewModel
            {
                Id = inputDTO.Id,
                CompanyName = inputDTO.CompanyName
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCPresentationLayer.Filters;
using MVCPresentationLayer.Models.Product;
using Swashbuckle.AspNetCore.Annotations;

namespace MVCPresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductAPIController> _logger;

        public ProductAPIController(IProductService productService, ILogger<ProductAPIController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(IEnumerable<ProductDTO>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Something went wrong!")]
        public async Task<IActionResult> GetProductsList(bool includeCategory = false, bool includeSupplier = false)
        {
            try
            {
                var productList = await _productService.GetAllProducts(includeCategory, includeSupplier);
                if (productList != null)
                {
                    return Ok(productList);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting list of all productss API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPut("{id}")]
        [ValidateModel]
        [SwaggerResponse((int)HttpStatusCode.OK, "Product with some identifier : was updated!")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product with some identifier : was not found!")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Something went wrong!")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductModel model)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with identifier : {id} was not found!");
                }

                var produtDTO = ConvertProductModelToDTO(model);
                if(produtDTO!=null)
                {
                    produtDTO.Id = id;
                    await _productService.UpdateProductAsync(produtDTO);

                    return Ok($"Product with identifier : {id} was updated!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in updating product identifier : {id} API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPost("")]
        [ValidateModel]
        [SwaggerResponse((int)HttpStatusCode.OK, "Product was created!")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Something went wrong!")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel model)
        {
            try
            {
                var produtDTO = ConvertProductModelToDTO(model);
                if (produtDTO != null)
                {
                    await _productService.CreateProductAsync(produtDTO);

                    return Ok($"Product was created!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in creating product API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Product with some identifier : was deleted!")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Product with some identifier : was not found!")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Something went wrong!")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with identifier : {id} was not found!");
                }

                await _productService.DeleteProductAsync(product);

                return Ok($"Product with identifier : {id} was deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in deleting product API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }

        private ProductDTO ConvertProductModelToDTO(ProductModel inputModel)
        {
            if (inputModel == null)
                return null;

            return new ProductDTO
            {
                ProductName = inputModel.ProductName,
                CategoryID = inputModel.CategoryId,
                QuantityPerUnit = inputModel.QuantityPerUnit,
                SupplierID = inputModel.SupplierId,
                UnitPrice = inputModel.UnitPrice,
                UnitsInStock = inputModel.UnitsInStock,
                UnitsOnOrder = inputModel.UnitsOnOrder,
                ReorderLevel = inputModel.ReorderLevel,
                Discontinued = inputModel.Discontinued
            };
        }
    }
}
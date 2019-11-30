using System;
using System.Threading.Tasks;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCPresentationLayer.Filters;
using MVCPresentationLayer.Models.Category;

namespace MVCPresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryAPIController> _logger;

        public CategoryAPIController(ICategoryService categoryService, ILogger<CategoryAPIController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategoriesList()
        {
            try
            {
                var categoryList = await _categoryService.GetAllCategories();
                if(categoryList!= null)
                {
                    return Ok(categoryList);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in getting list of all categories API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetCategoryImageById(int id)
        {
            try
            {
                var imageCategory = await _categoryService.GetPictureById(id);
                if (imageCategory == null)
                {
                    return NotFound($"Image of category with identifier : {id} was not found!");
                }

                return Ok(imageCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting image of category by id {id} API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }

        [HttpPut("image")]
        [ValidateModel]
        public async Task<IActionResult> UpdateImage([FromBody] CategoryPictureModel model)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(model.Id);
                if (category == null)
                {
                    return NotFound($"Category with identifier : {model.Id} was not found!");
                }

                await _categoryService.UpdatePictureById(model.Id, model.Picture);

                return Ok($"Category with identifier : {model.Id} was updated!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in update image of category identifier : {model.Id} API : {ex}");
            }

            return BadRequest("Something went wrong!");
        }
    }
}
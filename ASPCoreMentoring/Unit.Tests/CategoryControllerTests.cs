using BusinessLayer.DataTransferObject;
using BusinessLayer.Pagination;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MVCPresentationLayer.Configuration;
using MVCPresentationLayer.Controllers;
using MVCPresentationLayer.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class CategoryControllerTests
    {
        private Mock<ICategoryService> categoryServiceMock;
        private Mock<ILogger<CategoryController>> loggerMock;
        readonly IOptions<PaginationSection> configMock = Options.Create(new PaginationSection()
        { CountItemOnPage = 1, PageNumber = 1 });

        public CategoryControllerTests()
        {
            categoryServiceMock = new Mock<ICategoryService>();
            loggerMock = new Mock<ILogger<CategoryController>>();
        }

        [Fact]
        public async Task Category_Index_ReturnsViewResult_WithListCategories()
        {
            // Arrange
            var page = 1;

            var pagedResult = new PagedResult<CategoryDTO>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 0,
                Results = new List<CategoryDTO>
                {
                    new CategoryDTO
                    {
                        Id = 1,
                        CategoryName = "One",
                        Description = "Description 1"
                    },
                    new CategoryDTO
                    {
                        Id = 2,
                        CategoryName = "Two",
                        Description = "Description 2"
                    },
                    new CategoryDTO
                    {
                        Id = 3,
                        CategoryName = "Three",
                        Description = "Description 3"
                    }
                }
            };

            categoryServiceMock.Setup(x => x.GetPagedCategory(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(pagedResult);

            var controller = new CategoryController(categoryServiceMock.Object, configMock, loggerMock.Object);

            // Act
            var result = await controller.Index(page);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actual = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(3, actual.Count());
        }

        [Fact]
        public async Task Category_Edit_ReturnsViewResult_WithCategoryViewModel()
        {
            // Arrange
            var page = 1;

            var categoryDTO = new CategoryDTO
            {
                Id = 1,
                CategoryName = "One",
                Description = "Description 1"
            };

            categoryServiceMock.Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(categoryDTO);

            var controller = new CategoryController(categoryServiceMock.Object, configMock, loggerMock.Object);

            // Act
            var result = await controller.Edit(page);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actual = Assert.IsAssignableFrom<CategoryViewModel>(viewResult.ViewData.Model);
            Assert.Equal(categoryDTO.Id, actual.Id);
            Assert.Equal(categoryDTO.CategoryName, actual.CategoryName);
            Assert.Equal(categoryDTO.Description, actual.Description);
        }

        [Fact]
        public async Task Category_Picture_ReturnsFile_WithByteArray()
        {
            // Arrange
            var page = 1;

            var byteArray = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }; ;

            categoryServiceMock.Setup(x => x.GetPictureById(It.IsAny<int>()))
                .ReturnsAsync(byteArray);

            var controller = new CategoryController(categoryServiceMock.Object, configMock, loggerMock.Object);

            // Act
            var result = await controller.Picture(page);

            // Assert
            var contentResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal(byteArray, contentResult.FileContents);
        }

        [Fact]
        public async Task Category_PicturePost_ReturnsViewResult_WithCategoryViewModel()
        {
            // Arrange
            var categoryPictureViewModel = new CategoryPictureViewModel
            {
                Id = 1
            };

            var categoryDTO = new CategoryDTO
            {
                Id = 1,
                CategoryName = "One",
                Description = "Description 1",
                PictureLink = "Pictures Link 1"
            };

            categoryServiceMock.Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(categoryDTO);

            categoryServiceMock.Setup(x => x.UpdatePictureById(It.IsAny<int>(), It.IsAny<byte[]>()));

            var controller = new CategoryController(categoryServiceMock.Object, configMock, loggerMock.Object);

            // Act
            var result = await controller.Picture(categoryPictureViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actual = Assert.IsAssignableFrom<CategoryViewModel>(viewResult.ViewData.Model);
            Assert.Equal(categoryDTO.Id, actual.Id);
            Assert.Equal(categoryDTO.CategoryName, actual.CategoryName);
            Assert.Equal(categoryDTO.Description, actual.Description);
            Assert.Equal(categoryDTO.PictureLink, actual.PictureLink);
        }
    }
}
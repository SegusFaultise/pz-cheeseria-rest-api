using Microsoft.AspNetCore.Mvc;
using Moq;
using PZCheeseriaRestApi.Controllers;
using PZCheeseriaRestApi.Models.Dto;
using PZCheeseriaRestApi.Models;
using PZCheeseriaRestApi.Services;
using Xunit;

namespace PZCheeseriaRestApi.Tests
{
    public class CheeseProductControllerTests
    {
        private readonly CheeseProductController _controller;
        private readonly Mock<CheeseProductService> _mockService;

        public CheeseProductControllerTests()
        {
            _mockService = new Mock<CheeseProductService>();
            _controller = new CheeseProductController(_mockService.Object);
        }

        #region GetCheeseProductById Tests

        [Fact]
        public async Task GetCheeseProductById_ReturnsOk_WhenValidId()
        {
            var id = "671281bfeb54b465b98ee2ab";
            var cheeseProduct = new CheeseProductModel { 
                id = id, 
                cheese_product_name = "Cheddar",
                cheese_product_color = "Mild Yellow",
                cheese_product_description = "Old English Bite, with nutty after taste.",
                cheese_product_image_url = "https://example.com/images/cheddar.jpg",
                cheese_product_origin = "England",
                cheese_product_stock = 120,
                cheese_product_price_per_kilo = 25.50,
            };

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(cheeseProduct);

            var result = await _controller.GetCheeseProductById(id);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<CheeseProductModel>(okResult.Value);

            Assert.Equal(cheeseProduct.id, returnedProduct.id);
        }

        [Fact]
        public async Task GetCheeseProductById_ReturnsBadRequest_WhenIdIsNull()
        {
            var result = await _controller.GetCheeseProductById(null);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal("Cheese product id param null is null.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetCheeseProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var id = "invalid-id";
            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((CheeseProductModel)null);

            var result = await _controller.GetCheeseProductById(id);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal($"Cheese product with id {id} not found.", badRequestResult.Value);
        }

        #endregion

        #region PostCheeseProduct Tests

        [Fact]
        public async Task PostCheeseProduct_ReturnsCreatedAtAction_WhenProductIsValid()
        {
            var cheeseDto = new CheeseProductDto { 
                cheese_product_name = "Gouda", 
                cheese_product_price_per_kilo = 15.50,
                cheese_product_color = "Light Yellow",
                cheese_product_description = "Butter after taste.",
                cheese_product_image_url = "https://example.com/images/gouda.jpg",
                cheese_product_origin = "Netherlands",
                cheese_product_stock = 80
            };

            var cheeseProduct = CheeseProductModelMapper.ToModel(cheeseDto);

            _mockService.Setup(s => s.CreateAsync(It.IsAny<CheeseProductModel>())).Returns(Task.CompletedTask);

            var result = await _controller.PostCheeseProduct(cheeseDto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(CheeseProductController.Get), createdResult.ActionName);
        }

        [Fact]
        public async Task PostCheeseProduct_ReturnsBadRequest_WhenProductIsNull()
        {
            // Arrange
            CheeseProductDto cheeseDto = null;

            // Act
            var result = await _controller.PostCheeseProduct(cheeseDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal($"Cheese product body {cheeseDto} is null.", badRequestResult.Value);
        }

        #endregion

        #region UpdateCheeseProduct Tests

        [Fact]
        public async Task UpdateCheeseProduct_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            var id = "671281bfeb54b465b98ee2ac";
            var cheeseDto = new CheeseProductDto { 
                cheese_product_name = "Updated Cheese",
                cheese_product_price_per_kilo = 15.50,
                cheese_product_color = "Light Yellow",
                cheese_product_description = "Butter after taste.",
                cheese_product_image_url = "https://example.com/images/gouda.jpg",
                cheese_product_origin = "Netherlands",
                cheese_product_stock = 80
            };
            var existingProduct = new CheeseProductModel { 
                id = id, 
                cheese_product_name = "Old Cheese",
                cheese_product_price_per_kilo = 15.50,
                cheese_product_color = "Light Yellow",
                cheese_product_description = "Butter after taste.",
                cheese_product_image_url = "https://example.com/images/gouda.jpg",
                cheese_product_origin = "Netherlands",
                cheese_product_stock = 80
            };

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(existingProduct);
            _mockService.Setup(s => s.UpdateAsync(id, It.IsAny<CheeseProductModel>())).Returns(Task.CompletedTask);

            var result = await _controller.UpdateCheeseProduct(id, cheeseDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCheeseProduct_ReturnsBadRequest_WhenProductNotFound()
        {
            var id = "invalid-id";

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((CheeseProductModel)null);

            var result = await _controller.UpdateCheeseProduct(id, new CheeseProductDto {
                cheese_product_name = "Gouda",
                cheese_product_price_per_kilo = 15.50,
                cheese_product_color = "Light Yellow",
                cheese_product_description = "Butter after taste.",
                cheese_product_image_url = "https://example.com/images/gouda.jpg",
                cheese_product_origin = "Netherlands",
                cheese_product_stock = 80
            });
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Cheese product with id {id} not found.", badRequestResult.Value);
        }

        #endregion

        #region DeleteCheeseProduct Tests

        [Fact]
        public async Task DeleteCheeseProduct_ReturnsNoContent_WhenProductDeleted()
        {
            var id = "671281bfeb54b465b98ee2aw";
            var cheeseProduct = new CheeseProductModel { 
                id = id,
                cheese_product_name = "Cheddar",
                cheese_product_color = "Mild Yellow",
                cheese_product_description = "Old English Bite, with nutty after taste.",
                cheese_product_image_url = "https://example.com/images/cheddar.jpg",
                cheese_product_origin = "England",
                cheese_product_stock = 120,
                cheese_product_price_per_kilo = 25.50,
            };

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(cheeseProduct);
            _mockService.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCheeseProduct(id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCheeseProduct_ReturnsBadRequest_WhenProductNotFound()
        {
            var id = "invalid-id";

            _mockService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((CheeseProductModel)null);

            var result = await _controller.DeleteCheeseProduct(id);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal($"Cheese product with id {id} not found.", badRequestResult.Value);
        }

        #endregion
    }
}

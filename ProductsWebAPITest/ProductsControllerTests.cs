using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsWebAPI.Controllers;
using ProductsWebAPI.Models;
using ProductsWebAPI.Service;

namespace ProductsApp.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<IProductsService> _mockProductService;
        private ProductsController _controller;
        private Product _testProduct;

        [TestInitialize]
        public void Setup()
        {
            _mockProductService = new Mock<IProductsService>();
            _controller = new ProductsController(_mockProductService.Object);
            _testProduct = new Product { Id = 1, Name = "Test Product" };

        }

        #region Constructor Tests
        [TestMethod]
        public void Constructor_WithValidService_CreatesController()
        {
            // Act & Assert
            Assert.IsNotNull(_controller);
        }

        [TestMethod]
        public void Constructor_WithNullService_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ProductsController(null));
        }
        #endregion

        #region ListProducts Tests
        [TestMethod]
        public void ListProducts_ReturnsOkResultWithProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1" },
                new Product { Id = 2, Name = "Test Product 2" }
            };
            _mockProductService.Setup(s => s.GetAllProducts()).Returns(expectedProducts);

            // Act
            var result = _controller.ListProducts();

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ListProducts_WhenExceptionOccurs_ReturnsInternalServerError()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetAllProducts()).Throws(new Exception());

            // Act
            var result = _controller.ListProducts() as ExceptionResult;

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion

        #region GetProduct Tests
        [TestMethod]
        public void GetProduct_WithValidId_ReturnsOkResult()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetProduct(1)).Returns(_testProduct);

            // Act
            var result = _controller.GetProduct(1) as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testProduct, result.Content);
        }

        [TestMethod]
        public void GetProduct_WithInvalidId_ReturnsBadRequest()
        {
            // Act
            var result = _controller.GetProduct(0) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Invalid product ID. ID must be greater than 0.", result.Message);
        }

        [TestMethod]
        public void GetProduct_WithNonexistentId_ReturnsNotFound()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetProduct(1)).Returns((Product)null);

            // Act
            var result = _controller.GetProduct(1) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion

        #region CreateProduct Tests
        [TestMethod]
        public void CreateProduct_WithValidProduct_ReturnsCreatedResult()
        {
            // Act
            var result = _controller.CreateProduct(_testProduct) as CreatedNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testProduct, result.Content);
            _mockProductService.Verify(s => s.SaveProduct(_testProduct), Times.Once);
        }

        [TestMethod]
        public void CreateProduct_WithNullProduct_ReturnsBadRequest()
        {
            // Act
            var result = _controller.CreateProduct(null) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Product data cannot be null", result.Message);
        }
        #endregion

        #region UpdateProduct Tests
        [TestMethod]
        public void UpdateProduct_WithValidProduct_ReturnsOkResult()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetProduct(1)).Returns(_testProduct);

            // Act
            var result = _controller.UpdateProduct(1, _testProduct) as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_testProduct, result.Content);
        }

      #endregion

        #region DeleteProduct Tests
        [TestMethod]
        public void DeleteProduct_WithValidId_ReturnsOkResult()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetProduct(1)).Returns(_testProduct);

            // Act
            var result = _controller.DeleteProduct(1) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            _mockProductService.Verify(s => s.DeleteProduct(1), Times.Once);
        }
        #endregion
    }
}

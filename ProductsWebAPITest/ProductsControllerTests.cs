using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductsWebAPI.Controllers;
using ProductsWebAPI.Models;
using ProductsWebAPI.Service;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;


namespace ProductsAppTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private Mock<IProductsService> _mockProductService;
        private ProductsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockProductService = new Mock<IProductsService>();
            _controller = new ProductsController(_mockProductService.Object);
        }

        [TestMethod]
        public void ListProducts_ReturnsAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Test Product 1" },
                new Product { Id = 2, Name = "Test Product 2" }
            };
            _mockProductService.Setup(x => x.GetAllProducts()).Returns(expectedProducts);

            // Act
            var result = _controller.ListProducts();

            // Assert
            CollectionAssert.AreEqual(expectedProducts, result as List<Product>);
            _mockProductService.Verify(x => x.GetAllProducts(), Times.Once);
        }

        [TestMethod]
        public void GetProduct_WithValidId_ReturnsProduct()
        {
            // Arrange
            var expectedProduct = new Product { Id = 1, Name = "Test Product" };
            _mockProductService.Setup(x => x.GetProduct(1)).Returns(expectedProduct);

            // Act
            var result = _controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Product>));
            var okResult = result as OkNegotiatedContentResult<Product>;
            Assert.AreEqual(expectedProduct, okResult.Content);
        }

        [TestMethod]
        public void GetProduct_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockProductService.Setup(x => x.GetProduct(999)).Returns((Product)null);

            // Act
            var result = _controller.GetProduct(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateProduct_ValidProduct_CallsServiceSaveProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "New Product" };

            // Act
            _controller.CreateProduct(product);

            // Assert
            _mockProductService.Verify(x => x.SaveProduct(product), Times.Once);
        }

        [TestMethod]
        public void UpdateProduct_ValidProduct_CallsServiceUpdateProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Updated Product" };
            int id = 1;

            // Act
            _controller.UpdateProduct(id, product);

            // Assert
            _mockProductService.Verify(x => x.UpdateProduct(id, product), Times.Once);
        }

        [TestMethod]
        public void DeleteProduct_ValidId_CallsServiceDeleteProduct()
        {
            // Arrange
            int id = 1;

            // Act
            _controller.DeleteProduct(id);

            // Assert
            _mockProductService.Verify(x => x.DeleteProduct(id), Times.Once);
        }

        [TestMethod]
        public void ListProducts_WhenServiceReturnsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Product>();
            _mockProductService.Setup(x => x.GetAllProducts()).Returns(emptyList);

            // Act
            var result = _controller.ListProducts();

            // Assert
            CollectionAssert.AreEqual(emptyList, result as List<Product>);
        }

        [TestMethod]
        public void CreateProduct_NullProduct_HandlesGracefully()
        {
            // Arrange
            Product nullProduct = null;

            // Act & Assert
            _controller.CreateProduct(nullProduct); // Should not throw exception
            _mockProductService.Verify(x => x.SaveProduct(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void UpdateProduct_NullProduct_HandlesGracefully()
        {
            // Arrange
            Product nullProduct = null;
            int id = 1;

            // Act & Assert
            _controller.UpdateProduct(id, nullProduct); // Should not throw exception
            _mockProductService.Verify(x => x.UpdateProduct(id, It.IsAny<Product>()), Times.Once);
        }
    }
}





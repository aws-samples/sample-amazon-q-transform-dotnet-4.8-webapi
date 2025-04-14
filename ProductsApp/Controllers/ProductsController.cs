using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using ProductsApp.Models;
using ProductsApp.Service;


namespace ProductsApp.Controllers
{
    public class ProductsController : ApiController
    {

        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }


        [Route("api/products")]
        [HttpGet]
        public IEnumerable<Product> ListProducts()
        {
            //return products;
            return _productsService.GetAllProducts();
        }

        [Route("api/products/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {

            var product = _productsService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        // POST api/product
        [Route("api/products")]
        [HttpPost]
        public void CreateProduct([FromBody] Product value)
        {
            Debug.WriteLine(value);
            _productsService.SaveProduct(value);

        }

        // PUT api/product/5

        [Route("api/products/{id:int}")]
        [HttpPut]
        public void UpdateProduct(int id, [FromBody] Product newValue)
        {
            Debug.WriteLine(newValue);
            _productsService.UpdateProduct(id, newValue);

        }

        // DELETE api/products/5
        [Route("api/products/{id:int}")]
        [HttpDelete]
        public void DeleteProduct(int id)
        {
            _productsService.DeleteProduct(id);

        }
    }
}

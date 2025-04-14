using System.Collections.Generic;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Service
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);

        void SaveProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(int id, Product product);

    }
}

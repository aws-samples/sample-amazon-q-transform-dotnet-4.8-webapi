using System.Collections.Generic;
using ProductsApp.Models;

namespace ProductsApp.Service
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

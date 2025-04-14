using System.Collections.Generic;
using System.Linq;
using ProductsWebAPI.Models;
using ProductsWebAPI.Repository;

namespace ProductsWebAPI.Service
{
    public class ProductsService : IProductsService
    {
        public ProductsService() { }

        //Implement interface definitions
        public IEnumerable<Product> GetAllProducts()
        {
            //return products;
            using (var db = new ProductsContext())
            {
                return db.Products.ToArray();
            }


        }

        public Product GetProduct(int id)
        {
            using (var db = new ProductsContext())
            {
                Product query = (from p in db.Products
                                 where p.Id == id
                                 select p).FirstOrDefault();

                if (query == null)
                {
                    return null;
                }

                return query;
            }
        }

        public void SaveProduct(Product product)
        {
            using (var db = new ProductsContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var db = new ProductsContext())
            {
                Product value = (from p in db.Products
                                 where p.Id == id
                                 select p).FirstOrDefault();

                if (value == null)
                {
                    return;
                }

                db.Products.Remove(value);
                db.SaveChanges();
            }
        }

        public void UpdateProduct(int id, Product product)
        {
            using (var db = new ProductsContext())
            {
                Product value = (from p in db.Products
                                 where p.Id == id
                                 select p).FirstOrDefault();

                if (value == null)
                {
                    return;
                }

                value.Name = product.Name;
                value.Price = product.Price;
                value.Category = product.Category;

                db.SaveChanges();
            }
        }


    }
}
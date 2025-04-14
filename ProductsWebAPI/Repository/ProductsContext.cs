using System.Data.Entity;
using ProductsWebAPI.Models;

namespace ProductsWebAPI.Repository
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }


}
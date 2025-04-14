using System.Data.Entity;
using ProductsApp.Models;

namespace ProductsApp.Repository
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }


}
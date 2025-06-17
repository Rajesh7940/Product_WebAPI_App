using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Product_WebAPI_App.Models;

namespace Product_WebAPI_App.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
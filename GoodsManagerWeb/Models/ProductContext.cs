using GoodsLib.Entity;
using Microsoft.EntityFrameworkCore;

namespace GoodsManagerWeb.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductDto> Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> context)
            : base(context)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}

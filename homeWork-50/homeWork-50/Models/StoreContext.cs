using Microsoft.EntityFrameworkCore;

namespace homeWork_50.Models;

public class StoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Order> Orders { get; set; }

    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
}
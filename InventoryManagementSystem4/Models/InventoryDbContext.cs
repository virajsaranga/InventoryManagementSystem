using InventoryManagementSystem4.Models;
using System.Data.Entity;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext() : base("name=InventoryDBConnectionString")
    {
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
}

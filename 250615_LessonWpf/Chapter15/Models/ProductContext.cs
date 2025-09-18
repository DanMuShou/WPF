using Microsoft.EntityFrameworkCore;

namespace Chapter15.Models;

public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=WpfData;Username=merry_ma;Password=00000;"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var products = new List<Product>();
        for (var i = 0; i < 1000; i++)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = $"Product {i}",
                Description = $"This is product {i} description",
                Price = i * 10.0m,
            };
            products.Add(product);
        }

        modelBuilder.Entity<Product>().ToTable("T_Products").HasData(products);
    }
}

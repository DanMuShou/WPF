using Chapter15.Models;
using Microsoft.EntityFrameworkCore;

namespace Chapter15.Services;

public class ProductService(ProductContext context)
{
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public IEnumerable<Product> GetProducts()
    {
        return context.Products.ToList();
    }
}

using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext ctx) : IProductRepository
{
    public async Task<Product> AddAsync(Product product)
    {
        ctx.Product.Add(product);
        await ctx.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await ctx.Product
            .Include(p => p.Brand)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await ctx.Product
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        ctx.Product.Update(product);
        await ctx.SaveChangesAsync();
        return product;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var product = await ctx.Product.FindAsync(id);
        if (product is not null)
        {
            ctx.Product.Remove(product);
            await ctx.SaveChangesAsync();
        }
    }
}

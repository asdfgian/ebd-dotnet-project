using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> UpdateAsync(Product product);
    Task DeleteByIdAsync(int id);
}
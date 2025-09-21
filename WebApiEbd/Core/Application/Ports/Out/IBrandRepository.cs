using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IBrandRepository
    {
        Task<Brand?> AddAsync(Brand brand);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand?> GetByIdAsync(int id);
        Task<Brand?> UpdateAsync(Brand brand);
    }
}
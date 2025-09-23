using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IProviderRepository
    {
        Task<Provider?> AddAsync(Provider provider);
        Task<IEnumerable<Provider>> GetAllAsync();
        Task<Provider?> GetByIdAsync(int id);
        Task<Provider?> UpdateAsync(Provider provider);

    }
}
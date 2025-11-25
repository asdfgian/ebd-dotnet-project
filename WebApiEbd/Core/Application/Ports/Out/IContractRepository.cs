using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IContractRepository
    {
        Task<Contract> GetByIdAsync(int id);
        Task<IEnumerable<Contract>> GetAllAsync();
        Task<Contract> AddAsync(Contract contract);
        Task<Contract> UpdateAsync(Contract contract);
        Task DeleteByIdAsync(int id);
    }
}
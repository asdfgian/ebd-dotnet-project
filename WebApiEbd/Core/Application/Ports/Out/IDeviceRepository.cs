
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IDeviceRepository
    {
        Task<Device> AddAsync(Device device);
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
        Task<Device> UpdateAsync(Device device);
        Task DeleteByIdAsync(int id);
    }
}
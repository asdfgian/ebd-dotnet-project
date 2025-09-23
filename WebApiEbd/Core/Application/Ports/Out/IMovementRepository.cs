using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IMovementRepository
    {
        Task<Movement> AddAsync(Movement movement);
        Task<IEnumerable<Movement>> GetMovementsByDeviceIdAsync(int deviceId);
    }
}
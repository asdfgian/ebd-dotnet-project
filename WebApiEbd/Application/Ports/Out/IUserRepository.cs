using WebApiEbd.Domain.Models;

namespace WebApiEbd.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
    }
}
using WebApiEbd.Domain.Models;

namespace WebApiEbd.Application.Ports.Out
{
    public interface IUserAuthRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
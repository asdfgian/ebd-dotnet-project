using WebApiEbd.Domain.Models;

namespace WebApiEbd.Application.Ports.Out
{
    public interface IUserAuthRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string username);
        Task Add(User user);
    }
}
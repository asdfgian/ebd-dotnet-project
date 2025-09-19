using WebApiEbd.Domain.Models;

namespace WebApiEbd.Application.Ports.Out
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task Update(User user);
    }
}
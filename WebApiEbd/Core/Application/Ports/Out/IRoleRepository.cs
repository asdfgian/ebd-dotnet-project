using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out;

public interface IRoleRepository
{
    Task<Role> GetByIdAsync(int id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role> AddAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    Task DeleteByIdAsync(int id);
}

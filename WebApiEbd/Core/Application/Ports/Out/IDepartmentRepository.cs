using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out;

public interface IDepartmentRepository
{
    Task<Department> GetByIdAsync(int id);
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department> AddAsync(Department department);
    Task<Department> UpdateAsync(Department department);
    Task DeleteByIdAsync(int id);
}

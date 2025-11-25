using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> ListDepartments();
    Task<DepartmentDto> DepartmentById(int id);
    Task<DepartmentDto> CreateDepartment(string name);
    Task<DepartmentDto> UpdateDepartmentById(int id, string name);
    Task DeleteDepartmentById(int id);
}

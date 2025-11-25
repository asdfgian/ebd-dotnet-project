using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services;

public class DepartmentService(IDepartmentRepository repository) : IDepartmentService
{
    public async Task<IEnumerable<DepartmentDto>> ListDepartments()
    {
        var departments = await repository.GetAllAsync();
        return departments.Select(d => new DepartmentDto(d.Id, d.Name));
    }

    public async Task<DepartmentDto> DepartmentById(int id)
    {
        var department = await repository.GetByIdAsync(id) ??
                         throw new KeyNotFoundException($"Departamento con id {id} no encontrado.");
        return new DepartmentDto(department.Id, department.Name);
    }

    public async Task<DepartmentDto> CreateDepartment(string name)
    {
        var department = new Department { Name = name };
        var created = await repository.AddAsync(department);
        return new DepartmentDto(created.Id, created.Name);
    }

    public async Task<DepartmentDto> UpdateDepartmentById(int id, string name)
    {
        var department = await repository.GetByIdAsync(id) ??
                         throw new KeyNotFoundException($"Departamento con id {id} no encontrado.");
        department.Name = name;
        var updated = await repository.UpdateAsync(department);
        return new DepartmentDto(updated.Id, updated.Name);
    }

    public async Task DeleteDepartmentById(int id)
    {
        await repository.DeleteByIdAsync(id);
    }
}

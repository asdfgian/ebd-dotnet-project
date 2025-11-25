using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services;

public class RoleService(IRoleRepository repository) : IRoleService
{
    public async Task<IEnumerable<RoleDto>> ListRoles()
    {
        var roles = await repository.GetAllAsync();
        return roles.Select(r => new RoleDto(r.Id, r.Name, r.Description));
    }

    public async Task<RoleDto> RoleById(int id)
    {
        var role = await repository.GetByIdAsync(id) ??
                   throw new KeyNotFoundException($"Rol con id {id} no encontrado.");
        return new RoleDto(role.Id, role.Name, role.Description);
    }

    public async Task<RoleDto> CreateRole(string name, string description)
    {
        var role = new Role { Name = name, Description = description };
        var created = await repository.AddAsync(role);
        return new RoleDto(created.Id, created.Name, created.Description);
    }

    public async Task<RoleDto> UpdateRoleById(int id, string name, string description)
    {
        var role = await repository.GetByIdAsync(id) ??
                   throw new KeyNotFoundException($"Rol con id {id} no encontrado.");
        role.Name = name;
        role.Description = description;
        var updated = await repository.UpdateAsync(role);
        return new RoleDto(updated.Id, updated.Name, updated.Description);
    }

    public async Task DeleteRoleById(int id)
    {
        await repository.DeleteByIdAsync(id);
    }
}

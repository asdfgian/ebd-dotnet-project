using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> ListRoles();
    Task<RoleDto> RoleById(int id);
    Task<RoleDto> CreateRole(string name, string description);
    Task<RoleDto> UpdateRoleById(int id, string name, string description);
    Task DeleteRoleById(int id);
}

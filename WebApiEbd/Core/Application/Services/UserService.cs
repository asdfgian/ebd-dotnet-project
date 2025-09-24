using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Exceptions;

namespace WebApiEbd.Core.Application.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<IEnumerable<UserListDto>> ListUsers()
        {
            var users = await repository.GetAllAsync();
            return users.Select(u => new UserListDto(
                u.Id,
                u.Username,
                u.Name,
                u.Status,
                u.Role.Name
            ));
        }

        public async Task<UserDetailDto> UpdateUserById(int id, UpdateUserDto dto)
        {
            var user = await repository.GetByIdAsyncTracked(id) ?? throw new UserNotFoundException(id);

            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Phone))
                user.Phone = dto.Phone;

            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
                user.AvatarUrl = dto.AvatarUrl;

            user.DepartmentId = dto.DepartmentId;
            user.RoleId = dto.RoleId;
            user.Status = dto.Status;

            user.UpdatedAt = DateTime.Now;

            await repository.UpdateAsync(user);

            var updated = await repository.GetByIdAsync(user.Id) ?? throw new UserNotFoundException(id);

            return new UserDetailDto(
                updated.Id,
                updated.Email,
                updated.Username,
                updated.Name,
                updated.Phone,
                updated.Status,
                updated.Gender,
                updated.AvatarUrl,
                updated.Role.Name,
                updated.CreatedAt,
                updated.UpdatedAt,
                new DepartmentDto(updated.Department!.Id, updated.Department.Name),
                new RoleDto(updated.Role.Id, updated.Role.Name, updated.Role.Description)
            );
        }

        public async Task<UserDetailDto> UserDetailById(int id)
        {
            var user = await repository.GetByIdAsync(id) ?? throw new UserNotFoundException(id);
            return new UserDetailDto(
                user.Id,
                user.Email,
                user.Username,
                user.Name,
                user.Phone ?? "",
                user.Status,
                user.Gender,
                user.AvatarUrl,
                user.Role.Name,
                user.CreatedAt,
                user.UpdatedAt,
                new DepartmentDto(user.Department!.Id, user.Department.Name),
                new RoleDto(user.Role.Id, user.Role.Name, user.Role.Description)
            );
        }

    }
}
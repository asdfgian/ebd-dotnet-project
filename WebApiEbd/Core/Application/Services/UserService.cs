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
            var user = await repository.GetByIdAsync(id) ?? throw new UserNotFoundException(id);
            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;
            if (!string.IsNullOrWhiteSpace(dto.Phone))
                user.Phone = dto.Phone;

            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
                user.AvatarUrl = dto.AvatarUrl;

            if (dto.DepartmentId != null)
                user.DepartmentId = dto.DepartmentId;

            user.UpdatedAt = DateTime.Now;

            await repository.UpdateAsync(user);

            return new UserDetailDto(
                user.Id,
                user.Email,
                user.Username,
                user.Name,
                user.Phone,
                user.Status,
                user.Gender,
                user.AvatarUrl,
                user.Role.Name,
                user.CreatedAt,
                user.UpdatedAt,
                user.DepartmentId
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
                user.Phone,
                user.Status,
                user.Gender,
                user.AvatarUrl,
                user.Role.Name,
                user.CreatedAt,
                user.UpdatedAt,
                user.DepartmentId
            );
        }

    }
}
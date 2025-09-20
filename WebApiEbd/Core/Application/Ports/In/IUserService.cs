
using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In
{
    public interface IUserService
    {
        Task<IEnumerable<UserListDto>> ListUsers();

        Task<UserDetailDto> UserDetailById(int id);

        Task<UserDetailDto> UpdateUserById(int id, UpdateUserDto dto);
    }
}
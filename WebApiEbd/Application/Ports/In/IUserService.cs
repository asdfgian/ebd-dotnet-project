using WebApiEbd.Application.Dtos;

namespace WebApiEbd.Application.Ports.In
{
    public interface IUserService
    {
        Task<IEnumerable<UserListDto>> ListUsers();

        Task<UserDetailDto> UserDetailById(int id);

        Task<UserDetailDto> UpdateUserById(int id, UpdateUserDto dto);
    }
}

using WebApiEbd.Application.Dtos;

namespace WebApiEbd.Application.Ports.In
{
    public interface IUserAuthService
    {
        Task<AuthResultDto> SignIn(string identifier, string password);
        Task<AuthResultDto> SignUp(RegisterDto dto);
    }
}
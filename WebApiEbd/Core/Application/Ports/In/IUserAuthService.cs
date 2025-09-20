
using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In
{
    public interface IUserAuthService
    {
        Task<AuthResultDto> SignIn(string identifier, string password);
        Task<AuthResultDto> SignUp(RegisterDto dto);
    }
}
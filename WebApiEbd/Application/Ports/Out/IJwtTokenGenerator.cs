using WebApiEbd.Domain.Models;

namespace WebApiEbd.Application.Ports.Out
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
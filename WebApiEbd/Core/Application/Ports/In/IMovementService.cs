using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In
{
    public interface IMovementService
    {
        Task<MovementResponseDto> AddMovement(MovementRequestDto dto);
        Task<IEnumerable<MovementResponseDto>> ListMovementsByDeviceId(int deviceId);
    }
}
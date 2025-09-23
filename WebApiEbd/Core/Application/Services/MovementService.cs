using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services
{
    public class MovementService(IMovementRepository repository) : IMovementService
    {
        public async Task<MovementResponseDto> AddMovement(MovementRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Type))
                throw new ArgumentException("El tipo de movimiento es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Comment))
                throw new ArgumentException("El comentario del movimiento es obligatorio.");

            var movement = new Movement
            {
                Comment = dto.Comment.Trim(),
                Type = dto.Type.Trim(),
                DeviceId = dto.DeviceId,
                UserOriginId = dto.UserOriginId,
                UserDestinationId = dto.UserDestinationId,
                CreatedBy = dto.CreatedBy
            };

            var created = await repository.AddAsync(movement);

            return MapToDto(created);
        }

        public async Task<IEnumerable<MovementResponseDto>> ListMovementsByDeviceId(int deviceId)
        {
            var movements = await repository.GetMovementsByDeviceIdAsync(deviceId);

            if (!movements.Any())
                throw new KeyNotFoundException($"No se encontraron movimientos para el dispositivo con id {deviceId}.");

            return movements.Select(MapToDto);
        }

        private static MovementResponseDto MapToDto(Movement movement)
        {
            return new MovementResponseDto(
                movement.Date,
                movement.Comment ?? string.Empty,
                movement.DeviceId,
                movement.Type,
                movement.UserOriginId,
                movement.UserDestinationId,
                movement.CreatedBy
            );
        }
    }
}
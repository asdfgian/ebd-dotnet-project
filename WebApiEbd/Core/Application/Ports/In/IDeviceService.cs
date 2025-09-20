

using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In
{
    public interface IDeviceService
    {
        Task<IEnumerable<DeviceListDto>> ListDevices();
        Task<DeviceDetailDto> DeviceDetailById(int id);
        Task<DeviceDetailDto> CreateDevice(CreateDeviceDto dto);
        Task<DeviceDetailDto> UpdateUserById(int id, UpdateDeviceDto dto);
    }
}
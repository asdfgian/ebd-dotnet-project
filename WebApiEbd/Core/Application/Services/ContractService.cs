using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services;

public class ContractService(
    IContractRepository contractRepository,
    IDeviceRepository deviceRepository,
    IUserRepository userRepository,
    IProviderRepository providerRepository) : IContractService
{
    public async Task<IEnumerable<ContractListDto>> ListContracts()
    {
        var contracts = await contractRepository.GetAllAsync();
        return contracts.Select(c => new ContractListDto(
            c.Id,
            c.Title,
            c.StartDate,
            c.EndDate,
            c.Amount,
            c.Status,
            c.Provider.Name,
            c.User.Name ?? c.User.Username
        ));
    }

    public async Task<ContractDetailDto> ContractById(int id)
    {
        var contract = await contractRepository.GetByIdAsync(id) ??
                       throw new KeyNotFoundException($"Contrato con id {id} no encontrado.");

        var devices = contract.ContractsDevice.Select(cd => new ContractDeviceDto(
            cd.Device.Id,
            cd.Device.Name,
            cd.Device.SerialNumber,
            cd.RentalPrice
        ));

        return new ContractDetailDto(
            contract.Id,
            contract.Title,
            contract.StartDate,
            contract.EndDate,
            contract.Amount,
            contract.Status,
            contract.Route,
            contract.CreatedAt,
            contract.UpdatedAt,
            new ProviderDetailDto(
                contract.Provider.Id,
                contract.Provider.Ruc,
                contract.Provider.Name,
                contract.Provider.Address,
                contract.Provider.District,
                contract.Provider.Province,
                contract.Provider.Department,
                contract.Provider.Status,
                contract.Provider.Email,
                contract.Provider.Phone
            ),
            new UserDetailDto(
                contract.User.Id,
                contract.User.Email,
                contract.User.Username,
                contract.User.Name ?? string.Empty,
                contract.User.Phone,
                contract.User.Status,
                contract.User.Gender,
                contract.User.AvatarUrl,
                contract.User.Role.Name,
                contract.User.CreatedAt,
                contract.User.UpdatedAt,
                contract.User.Department != null ? new DepartmentDto(
                    contract.User.Department.Id,
                    contract.User.Department.Name
                ) : null!,
                new RoleDto(
                    contract.User.Role.Id,
                    contract.User.Role.Name,
                    contract.User.Role.Description
                )
            ),
            devices
        );
    }

    public async Task<ContractDetailDto> CreateContract(CreateContractDto dto)
    {
        var provider = await providerRepository.GetByIdAsync(dto.ProviderId) ??
                       throw new KeyNotFoundException($"Proveedor con id {dto.ProviderId} no encontrado.");

        var user = await userRepository.GetByIdAsync(dto.UserId) ??
                   throw new KeyNotFoundException($"Usuario con id {dto.UserId} no encontrado.");

        var contract = new Contract
        {
            Title = dto.Title,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Amount = dto.Amount,
            ProviderId = dto.ProviderId,
            UserId = dto.UserId,
            OrderId = dto.OrderId,
            Route = dto.Route,
            Status = "PLACED"
        };

        var created = await contractRepository.AddAsync(contract);

        // Agregar dispositivos al contrato
        if (dto.Devices != null && dto.Devices.Count > 0)
        {
            foreach (var deviceItem in dto.Devices)
            {
                var device = await deviceRepository.GetByIdAsync(deviceItem.DeviceId) ??
                             throw new KeyNotFoundException($"Dispositivo con id {deviceItem.DeviceId} no encontrado.");

                created.ContractsDevice.Add(new ContractsDevice
                {
                    ContractId = created.Id,
                    DeviceId = deviceItem.DeviceId,
                    RentalPrice = deviceItem.RentalPrice
                });
            }

            await contractRepository.UpdateAsync(created);
        }

        return await ContractById(created.Id);
    }

    public async Task<ContractDetailDto> UpdateContractById(int id, UpdateContractDto dto)
    {
        var contract = await contractRepository.GetByIdAsync(id) ??
                       throw new KeyNotFoundException($"Contrato con id {id} no encontrado.");

        if (!string.IsNullOrWhiteSpace(dto.Title))
            contract.Title = dto.Title;

        if (dto.StartDate.HasValue)
            contract.StartDate = dto.StartDate.Value;

        if (dto.EndDate.HasValue)
            contract.EndDate = dto.EndDate.Value;

        if (dto.Amount.HasValue)
            contract.Amount = dto.Amount.Value;

        if (dto.ProviderId.HasValue)
        {
            var provider = await providerRepository.GetByIdAsync(dto.ProviderId.Value) ??
                           throw new KeyNotFoundException($"Proveedor con id {dto.ProviderId.Value} no encontrado.");
            contract.ProviderId = dto.ProviderId.Value;
        }

        if (!string.IsNullOrWhiteSpace(dto.Status))
            contract.Status = dto.Status;

        if (!string.IsNullOrWhiteSpace(dto.Route))
            contract.Route = dto.Route;

        if (dto.Devices != null && dto.Devices.Count > 0)
        {
            contract.ContractsDevice.Clear();
            foreach (var deviceItem in dto.Devices)
            {
                var device = await deviceRepository.GetByIdAsync(deviceItem.DeviceId) ??
                             throw new KeyNotFoundException($"Dispositivo con id {deviceItem.DeviceId} no encontrado.");

                contract.ContractsDevice.Add(new ContractsDevice
                {
                    ContractId = contract.Id,
                    DeviceId = deviceItem.DeviceId,
                    RentalPrice = deviceItem.RentalPrice
                });
            }
        }

        var updated = await contractRepository.UpdateAsync(contract);
        return await ContractById(updated.Id);
    }

    public async Task DeleteContractById(int id)
    {
        await contractRepository.DeleteByIdAsync(id);
    }
}

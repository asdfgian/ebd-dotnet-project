using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface IContractService
{
    Task<IEnumerable<ContractListDto>> ListContracts();
    Task<ContractDetailDto> ContractById(int id);
    Task<ContractDetailDto> CreateContract(CreateContractDto dto);
    Task<ContractDetailDto> UpdateContractById(int id, UpdateContractDto dto);
    Task DeleteContractById(int id);
}
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out;

public interface ICountryOriginRepository
{
    Task<CountryOrigin> GetByIdAsync(int id);
    Task<IEnumerable<CountryOrigin>> GetAllAsync();
    Task<CountryOrigin> AddAsync(CountryOrigin countryOrigin);
    Task<CountryOrigin> UpdateAsync(CountryOrigin countryOrigin);
    Task DeleteByIdAsync(int id);
}

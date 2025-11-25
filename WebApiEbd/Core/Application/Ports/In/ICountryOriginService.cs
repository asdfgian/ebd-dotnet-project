using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface ICountryOriginService
{
    Task<IEnumerable<CountryOriginDto>> ListCountries();
    Task<CountryOriginDto> CountryById(int id);
    Task<CountryOriginDto> CreateCountry(CreateCountryOriginDto dto);
    Task<CountryOriginDto> UpdateCountryById(int id, UpdateCountryOriginDto dto);
    Task DeleteCountryById(int id);
}

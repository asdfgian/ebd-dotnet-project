using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services;

public class CountryOriginService(ICountryOriginRepository repository) : ICountryOriginService
{
    public async Task<IEnumerable<CountryOriginDto>> ListCountries()
    {
        var countries = await repository.GetAllAsync();
        return countries.Select(c => new CountryOriginDto(c.Id, c.Name));
    }

    public async Task<CountryOriginDto> CountryById(int id)
    {
        var country = await repository.GetByIdAsync(id) ??
                      throw new KeyNotFoundException($"País con id {id} no encontrado.");
        return new CountryOriginDto(country.Id, country.Name);
    }

    public async Task<CountryOriginDto> CreateCountry(CreateCountryOriginDto dto)
    {
        var country = new CountryOrigin { Name = dto.Name };
        var created = await repository.AddAsync(country);
        return new CountryOriginDto(created.Id, created.Name);
    }

    public async Task<CountryOriginDto> UpdateCountryById(int id, UpdateCountryOriginDto dto)
    {
        var country = await repository.GetByIdAsync(id) ??
                      throw new KeyNotFoundException($"País con id {id} no encontrado.");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            country.Name = dto.Name;

        var updated = await repository.UpdateAsync(country);
        return new CountryOriginDto(updated.Id, updated.Name);
    }

    public async Task DeleteCountryById(int id)
    {
        await repository.DeleteByIdAsync(id);
    }
}

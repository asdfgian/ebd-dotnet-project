using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services
{
    public class BrandService(IBrandRepository repository) : IBrandService
    {
        public async Task<BrandDto> BrandById(int id)
        {
            var brand = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"No se encontró la marca con id {id}");
            return new BrandDto(
                brand.Id,
                brand.Name,
                brand.CountryOrigin.Name
            );
        }

        public async Task<BrandDto> CreateBrand(CreateBrandDto dto)
        {
            var brand = new Brand
            {
                Name = dto.Name,
                CountryOriginId = dto.CountryOriginId
            };

            var created = await repository.AddAsync(brand) ?? throw new InvalidOperationException("No se pudo crear la marca.");
            return new BrandDto(
                created.Id,
                created.Name,
                created.CountryOrigin.Name
            );
        }

        public async Task<IEnumerable<BrandDto>> ListBrands()
        {
            var brands = await repository.GetAllAsync();
            return brands.Select(b => new BrandDto(
                b.Id,
                b.Name,
                b.CountryOrigin.Name
            ));
        }

        public async Task<BrandDto> UpdateBrandById(int id, UpdateBrandDto dto)
        {
            var brand = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"No se encontró la marca con id {id}");
            brand.Name = dto.Name;
            brand.CountryOriginId = dto.CountryOriginId;

            var updated = await repository.UpdateAsync(brand) ?? throw new InvalidOperationException("No se pudo actualizar la marca.");
            return new BrandDto(
                updated.Id,
                updated.Name,
                updated.CountryOrigin.Name
            );
        }
    }
}
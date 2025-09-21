using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> ListBrands();

        Task<BrandDto> BrandById(int id);

        Task<BrandDto> CreateBrand(CreateBrandDto dto);

        Task<BrandDto> UpdateBrandById(int id, UpdateBrandDto dto);
    }
}
using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface IProductService
{
    Task<IEnumerable<ProductListDto>> ListProducts();
    Task<ProductDetailDto> ProductDetailById(int id);
    Task<ProductDetailDto> CreateProduct(CreateProductDto dto);
    Task<ProductDetailDto> UpdateProductById(int id, UpdateProductDto dto);
    Task DeleteProductById(int id);
}

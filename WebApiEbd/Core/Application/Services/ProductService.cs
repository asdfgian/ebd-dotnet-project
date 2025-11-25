using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IEnumerable<ProductListDto>> ListProducts()
    {
        var products = await repository.GetAllAsync();
        return products.Select(p => new ProductListDto(
            p.Id,
            p.Name,
            p.Model,
            p.Brand?.Name ?? string.Empty
        ));
    }

    public async Task<ProductDetailDto> ProductDetailById(int id)
    {
        var product = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Product with ID {id} not found.");

        return new ProductDetailDto(
            product.Id,
            product.Name,
            product.Description ?? string.Empty,
            product.Model,
            product.BrandId,
            product.Brand?.Name ?? string.Empty
        );
    }

    public async Task<ProductDetailDto> CreateProduct(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Model = dto.Model,
            BrandId = dto.BrandId
        };
        
        var created = await repository.AddAsync(product);
        
        return new ProductDetailDto(
            created.Id,
            created.Name,
            created.Description ?? string.Empty,
            created.Model,
            created.BrandId,
            created.Brand.Name
        );
    }

    public async Task<ProductDetailDto> UpdateProductById(int id, UpdateProductDto dto)
    {
        var existing = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Producto con id {id} no encontrado.");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            existing.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.Description))
            existing.Description = dto.Description;
        if (!string.IsNullOrWhiteSpace(dto.Model))
            existing.Model = dto.Model;
        if (dto.BrandId.HasValue)
            existing.BrandId = dto.BrandId.Value;

        var updated = await repository.UpdateAsync(existing);

        return new ProductDetailDto(
            updated.Id,
            updated.Name,
            updated.Description ?? string.Empty,
            updated.Model,
            updated.BrandId,
            updated.Brand?.Name ?? string.Empty
        );
    }

    public async Task DeleteProductById(int id)
    {
        await repository.DeleteByIdAsync(id);
    }
}

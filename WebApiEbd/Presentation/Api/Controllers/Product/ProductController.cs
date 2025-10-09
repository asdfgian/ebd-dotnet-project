using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Product;

[Route("[controller]")]
[ApiController]

public class ProductController(IProductService service) : ControllerBase
{
    // GET: product/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ProductListDto>>> GetAll()
    {
        var products = await service.ListProducts();
        return Ok(products);
    }

    // GET: product/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDetailDto>> GetById(int id)
    {
        var product = await service.ProductDetailById(id);
        return Ok(product);
    }

    // POST: product
    [HttpPost]
    public async Task<ActionResult<ProductDetailDto>> Create([FromBody] CreateProductDto dto)
    {
        var created = await service.CreateProduct(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: product/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDetailDto>> Update(int id, [FromBody] UpdateProductDto dto)
    {
        var updated = await service.UpdateProductById(id, dto);
        return Ok(updated);
    }

    // DELETE: product/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteProductById(id);
        return NoContent();
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.PurchaseOrder;

[Route("[controller]")]
[ApiController]
[Authorize]
public class PurchaseOrderController(IPurchaseOrderService service) : ControllerBase
{
    // GET: purchaseorder/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var orders = await service.ListPurchaseOrders();
        return Ok(orders);
    }

    // GET: purchaseorder/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await service.PurchaseOrderById(id);
        return Ok(order);
    }

    // POST: purchaseorder
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderDto dto)
    {
        var created = await service.CreatePurchaseOrder(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: purchaseorder/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePurchaseOrderDto dto)
    {
        var updated = await service.UpdatePurchaseOrderById(id, dto);
        return Ok(updated);
    }

    // DELETE: purchaseorder/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeletePurchaseOrderById(id);
        return NoContent();
    }
}

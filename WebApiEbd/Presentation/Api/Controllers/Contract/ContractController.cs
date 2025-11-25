using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Contract;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ContractController(IContractService service) : ControllerBase
{
    // GET: contract/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var contracts = await service.ListContracts();
        return Ok(contracts);
    }

    // GET: contract/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contract = await service.ContractById(id);
        return Ok(contract);
    }

    // POST: contract
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractDto dto)
    {
        var created = await service.CreateContract(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: contract/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContractDto dto)
    {
        var updated = await service.UpdateContractById(id, dto);
        return Ok(updated);
    }

    // DELETE: contract/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteContractById(id);
        return NoContent();
    }
}

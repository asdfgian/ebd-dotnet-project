using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEbd.Presentation.Api.Controllers.Contract;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ContractController : ControllerBase
{
    
}
using Microsoft.AspNetCore.Mvc;
using Movement.Application.Network.Apis.VirtualOffice;

namespace Movement.Api.Controllers;

[Controller]
[Route("api/virtual-office")]
public class VirtualOfficeController(IVirtualOfficeHttpService virtualOfficeHttpService) : ControllerBase
{
    [HttpGet("user/{pinfl}")]
    public async Task<IActionResult> GetVirtualOffice([FromRoute] string pinfl)
    {
        var result = await virtualOfficeHttpService.GetUserByPinfl(pinfl);
        return Ok(result);
    }
}
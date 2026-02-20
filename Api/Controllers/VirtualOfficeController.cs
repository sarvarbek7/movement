using Microsoft.AspNetCore.Mvc;
using Movement.Application.Network.Apis.VirtualOffice;

namespace Movement.Api.Controllers;

[Controller]
[Route("api/virtual-office")]
public class VirtualOfficeController(IVirtualOfficeHttpService virtualOfficeHttpService) : ControllerBase
{
    [HttpGet("user/{pinfl}")]
    public async Task<IActionResult> GetVirtualOffice([FromRoute] string pinfl, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetUserByPinfl(pinfl, cancellationToken);
        return Ok(result);
    }

    [HttpGet("users/{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetUserById(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("department/{id:int}")]
    public async Task<IActionResult> GetDepartmentById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetDepartmentById(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("workplace/{id:int}")]
    public async Task<IActionResult> GetWorkplaceById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetWorkplaceById(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet("station/{code}")]
    public async Task<IActionResult> GetStationByCode([FromRoute] string code, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetStationByCode(code, cancellationToken);
        return Ok(result);
    }
}
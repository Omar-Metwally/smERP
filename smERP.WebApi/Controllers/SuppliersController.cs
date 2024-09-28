using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Suppliers.Commands.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class SuppliersController : AppControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddSupplierCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EditSupplierCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteSupplierCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}

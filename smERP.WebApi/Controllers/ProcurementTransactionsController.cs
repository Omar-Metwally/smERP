using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Branches.Queries.Models;
using smERP.Application.Features.Categories.Commands.Models;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;
using smERP.Application.Features.ProcurementTransactions.Queries.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProcurementTransactionsController : AppControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddProcurementTransactionCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EditProcurementTransactionCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginatedProcurementTransactions([FromQuery] GetPaginatedProcurementTransactionQuery request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}

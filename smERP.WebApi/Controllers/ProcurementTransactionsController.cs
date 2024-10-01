using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Categories.Commands.Models;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;

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
}

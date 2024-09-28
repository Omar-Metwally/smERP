using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Categories.Commands.Models;
using smERP.Application.Features.Categories.Queries.Models;
using smERP.Application.Features.Companies.Commands.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : AppControllerBase
{
    [HttpGet("{request.CategoryId}")]
    public async Task<IActionResult> GetById([FromRoute] GetCategoryByIdQuery request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCategoryCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EditCategoryCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}
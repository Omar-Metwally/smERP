using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Brands.Commands.Models;
using smERP.Application.Features.Categories.Queries.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class BrandsController : AppControllerBase
{
    //[HttpGet("{request.CategoryId}")]
    //public async Task<IActionResult> GetById([FromRoute] GetBrand request)
    //{
    //    var response = await Mediator.Send(request);
    //    var apiResult = response.ToApiResult();
    //    return StatusCode(apiResult.StatusCode, apiResult);
    //}

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddBrandCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EditBrandCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteBrandCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}

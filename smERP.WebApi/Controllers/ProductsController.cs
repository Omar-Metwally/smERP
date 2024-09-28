using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Attributes.Commands.Models;
using smERP.Application.Features.ProductInstances.Commands.Models;
using smERP.Application.Features.Products.Commands.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : AppControllerBase
{
    //[HttpGet("{request.CategoryId}")]
    //public async Task<IActionResult> GetById([FromRoute] GetBrand request)
    //{
    //    var response = await Mediator.Send(request);
    //    var apiResult = response.ToApiResult();
    //    return StatusCode(apiResult.StatusCode, apiResult);
    //}

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] AddProductCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] EditProductCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost("{request.ProductId}/instances")]
    public async Task<IActionResult> CreateProductInstance([FromBody] AddProductInstanceCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut("{request.ProductId}/instances")]
    public async Task<IActionResult> UpdateProductInstance([FromBody] EditProductInstanceCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete("{request.ProductId}/instances")]
    public async Task<IActionResult> DeleteProductInstance([FromBody] DeleteProductInstanceCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

}

using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Attributes.Commands.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AttributesController : AppControllerBase
{
    //[HttpGet("{request.CategoryId}")]
    //public async Task<IActionResult> GetById([FromRoute] GetBrand request)
    //{
    //    var response = await Mediator.Send(request);
    //    var apiResult = response.ToApiResult();
    //    return StatusCode(apiResult.StatusCode, apiResult);
    //}

    [HttpPost]
    public async Task<IActionResult> CreateAttribute([FromBody] AddAttributeCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAttribute([FromBody] EditAttributeCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAttribute([FromBody] DeleteAttributeCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost("{request.AttributeId}/values")]
    public async Task<IActionResult> CreateAttributeValue([FromBody] AddAttributeValueCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut("{request.AttributeId}/values/{request.AttributeValueId}")]
    public async Task<IActionResult> UpdateAttributeValue([FromBody] EditAttributeValueCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete("{request.AttributeId}/values/{request.AttributeValueId}")]
    public async Task<IActionResult> DeleteAttributeValue([FromBody] DeleteAttributeValueCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}

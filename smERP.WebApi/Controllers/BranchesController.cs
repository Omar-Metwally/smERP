using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Attributes.Commands.Models;
using smERP.Application.Features.Branches.Commands.Models;
using smERP.Application.Features.StorageLocations.Comands.Models;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class BranchesController : AppControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBranch([FromBody] AddBranchCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBranch([FromBody] EditBranchCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBranch([FromBody] DeleteBranchCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost("{request.BranchId}/storage-location")]
    public async Task<IActionResult> CreateStorageLocation([FromBody] AddStorageLocationCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPut("{request.BranchId}/storage-location/{request.StorageLocationId}")]
    public async Task<IActionResult> UpdateStorageLocation([FromBody] EditStorageLocationCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpDelete("{request.BranchId}/storage-location/{request.StorageLocationId}")]
    public async Task<IActionResult> DeleteStorageLocation([FromBody] DeleteStorageLocationCommandModel request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

}

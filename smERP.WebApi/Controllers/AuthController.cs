using Microsoft.AspNetCore.Mvc;
using smERP.Application.Features.Auth.Commands.Models;
using smERP.Application.Features.Auth.Commands.Results;
using smERP.SharedKernel.Responses;

namespace smERP.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : AppControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterCommandModel<IResult<RegisterResult>> request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginCommandModel<IResult<LoginResult>> request)
    {
        var response = await Mediator.Send(request);
        var apiResult = response.ToApiResult();
        return StatusCode(apiResult.StatusCode, apiResult);
    }
}

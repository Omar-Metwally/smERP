using MediatR;
using Microsoft.AspNetCore.Http;
using smERP.Application.Contracts.Infrastructure.Identity;
using smERP.Application.Features.Auth.Commands.Models;
using smERP.Application.Features.Auth.Commands.Results;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Commands.Handlers;

public class AuthCommandHandler(IAuthService authService) :
    IRequestHandler<RegisterCommandModel<IResult<RegisterResult>>, IResult<RegisterResult>>,
    IRequestHandler<LoginCommandModel<IResult<LoginResult>>, IResult<LoginResult>>

{
    private readonly IAuthService _authService = authService;

    public async Task<IResult<RegisterResult>> Handle(RegisterCommandModel<IResult<RegisterResult>> request, CancellationToken cancellationToken)
    {
        return await _authService.Register(request);
    }

    public async Task<IResult<LoginResult>> Handle(LoginCommandModel<IResult<LoginResult>> request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request);
    }
}
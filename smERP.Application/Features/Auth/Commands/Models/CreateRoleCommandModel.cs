using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Commands.Models;

public record CreateRoleCommandModel(string RoleName) : IRequest<IResult<string>>;

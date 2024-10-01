using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Commands.Models;

public record LoginCommandModel(string Email, string Password) : IRequest<IResultBase>;

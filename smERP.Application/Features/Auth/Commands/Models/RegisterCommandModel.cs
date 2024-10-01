using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Commands.Models;

public record RegisterCommandModel(int BranchId, string FirstName, string LastName, string Email, string Password) : IRequest<IResultBase>;

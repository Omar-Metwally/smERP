using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Commands.Models;

public record RegisterCommandModel<TResult>(
    int BranchId,
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<TResult>
    where TResult : IResultBase;
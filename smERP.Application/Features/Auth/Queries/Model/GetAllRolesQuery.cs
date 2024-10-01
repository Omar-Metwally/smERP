using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Queries.Model;

public class GetAllRolesQuery : IRequest<IResult<IEnumerable<string>>>
{
}

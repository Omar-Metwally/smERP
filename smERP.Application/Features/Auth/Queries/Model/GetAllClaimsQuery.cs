using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.Auth.Queries.Model;

public class GetAllClaimsQuery : IRequest<IResult<IEnumerable<string>>>
{
}

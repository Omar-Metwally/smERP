using MediatR;
using smERP.Application.Contracts.Persistence;
using smERP.Application.Features.ProcurementTransactions.Queries.Models;
using smERP.Application.Features.ProcurementTransactions.Queries.Responses;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.ProcurementTransactions.Queries.Handlers;

public class ProcurementTransactionQueryHandler(IProcurementTransactionRepository procurementTransactionRepository) :
    IRequestHandler<GetPaginatedProcurementTransactionQuery, IResult<PagedResult<GetPaginatedProcurementTransactionQueryResponse>>>
{
    private readonly IProcurementTransactionRepository _procurementTransactionRepository = procurementTransactionRepository;

    public async Task<IResult<PagedResult<GetPaginatedProcurementTransactionQueryResponse>>> Handle(GetPaginatedProcurementTransactionQuery request, CancellationToken cancellationToken)
    {
        var paginatedTransactions = await _procurementTransactionRepository.GetPagedAsync(request);
        return new Result<PagedResult<GetPaginatedProcurementTransactionQueryResponse>>(paginatedTransactions);
    }
}

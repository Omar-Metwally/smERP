using smERP.Domain.Entities.InventoryTransaction;
using smERP.Application.Contracts.Persistence;
using smERP.Persistence.Data;
using smERP.SharedKernel.Responses;
using smERP.Application.Features.ProcurementTransactions.Queries.Responses;
using Microsoft.EntityFrameworkCore;
using smERP.Application.Features.Brands.Queries.Responses;
using smERP.Domain.Entities.Product;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;

namespace smERP.Persistence.Repositories;

public class ProcurementTransactionRepository(ProductDbContext context) : Repository<ProcurementTransaction>(context), IProcurementTransactionRepository
{
    public new async Task<PagedResult<GetPaginatedProcurementTransactionQueryResponse>> GetPagedAsync(PaginationParameters parameters)
    {
        var query = _context.Set<ProcurementTransaction>().AsNoTracking().AsQueryable();

        var filteredQuery = ApplyFilters(query, parameters);
        var totalCount = await filteredQuery.CountAsync();

        var sortedQuery = ApplySorting(filteredQuery, parameters);

        var paginatedQuery = sortedQuery
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        var projectedQuery = paginatedQuery.Select(b => new GetPaginatedProcurementTransactionQueryResponse(
            b.Id,
            b.Supplier.Name.English,
            b.StorageLocation.Branch.Name.English,
            b.StorageLocation.Name,
            b.TransactionDate,
            b.Payments.Select(payment => new Payment(payment.PayedAmount, payment.PaymentMethod)),
            b.Items.Select(item => new TransactionItem(
                item.ProductInstanceId,
                item.ProductInstance.Product.Name.English,
                item.Quantity,
                item.UnitPrice,
                item.InventoryTransactionItemUnits != null
                    ? item.InventoryTransactionItemUnits.Select(unit => new ProductUnit(unit.SerialNumber))
                    : null
            ))
        ));

        var pagedData = await projectedQuery.ToListAsync();

        return new PagedResult<GetPaginatedProcurementTransactionQueryResponse>
        {
            TotalCount = totalCount,
            PageSize = parameters.PageSize,
            PageNumber = parameters.PageNumber,
            Data = pagedData
        };
    }

    private static IQueryable<ProcurementTransaction> ApplyFilters(IQueryable<ProcurementTransaction> query, PaginationParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.SearchTerm))
        {
            query = query.Where(b =>
                EF.Functions.Like(b.Supplier.Name.English, $"%{parameters.SearchTerm}%") ||
                EF.Functions.Like(b.StorageLocation.Name, $"%{parameters.SearchTerm}%") ||
                EF.Functions.Like(b.StorageLocation.Branch.Name.English, $"%{parameters.SearchTerm}%") ||
                b.Items.Any(x => EF.Functions.Like(x.ProductInstance.Product.Name.English, $"%{parameters.SearchTerm}%")));
        }

        if (parameters.StartDate.HasValue)
        {
            query = query.Where(b => b.TransactionDate >= parameters.StartDate.Value);
        }

        if (parameters.EndDate.HasValue)
        {
            query = query.Where(b => b.TransactionDate <= parameters.EndDate.Value);
        }

        return query;
    }

    private static IQueryable<ProcurementTransaction> ApplySorting(IQueryable<ProcurementTransaction> query, PaginationParameters parameters)
    {
        if (!string.IsNullOrEmpty(parameters.SortBy))
        {
            switch (parameters.SortBy.ToLower())
            {
                case "transactiondate":
                    query = parameters.SortDescending
                        ? query.OrderByDescending(b => b.TransactionDate)
                        : query.OrderBy(b => b.TransactionDate);
                    break;
                case "supplier":
                    query = parameters.SortDescending
                        ? query.OrderByDescending(b => b.Supplier.Name.English)
                        : query.OrderBy(b => b.Supplier.Name.English);
                    break;
                case "storagelocation":
                    query = parameters.SortDescending
                        ? query.OrderByDescending(b => b.StorageLocation.Name)
                        : query.OrderBy(b => b.StorageLocation.Name);
                    break;
                case "branch":
                    query = parameters.SortDescending
                        ? query.OrderByDescending(b => b.StorageLocation.Branch.Name.English)
                        : query.OrderBy(b => b.StorageLocation.Branch.Name.English);
                    break;
                case "createdat":
                    query = parameters.SortDescending
                        ? query.OrderByDescending(b => b.CreatedAt)
                        : query.OrderBy(b => b.CreatedAt);
                    break;
            }
        }
        else
        {
            query = query.OrderBy(b => b.Id);
        }

        return query;
    }

}
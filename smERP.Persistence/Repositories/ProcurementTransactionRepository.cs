using smERP.Domain.Entities.InventoryTransaction;
using smERP.Application.Contracts.Persistence;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class ProcurementTransactionRepository(ProductDbContext context) : Repository<ProcurementTransaction>(context), IProcurementTransactionRepository
{
}
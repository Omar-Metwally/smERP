using smERP.Domain.Entities.InventoryTransaction;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class ProcurementTransactionRepository(ProductDbContext context) : Repository<ProcurementTransaction>(context), IProcurementTransactionRepository
{
}
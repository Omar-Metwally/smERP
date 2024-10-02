using smERP.Domain.Entities.ExternalEntities;
using smERP.Application.Contracts.Persistence;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class SupplierRepository(ProductDbContext context) : Repository<Supplier>(context), ISupplierRepository
{
}

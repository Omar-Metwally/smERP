using smERP.Domain.Entities.ExternalEntities;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class SupplierRepository(ProductDbContext context) : Repository<Supplier>(context), ISupplierRepository
{
}

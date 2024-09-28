using smERP.Domain.Entities.Product;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class BrandRepository(ProductDbContext context) : Repository<Brand>(context), IBrandRepository
{
}
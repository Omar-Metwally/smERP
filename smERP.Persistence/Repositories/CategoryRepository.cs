using smERP.Domain.Entities.Product;
using smERP.Application.Contracts.Persistence;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class CategoryRepository(ProductDbContext context) : Repository<Category>(context), ICategoryRepository
{
}

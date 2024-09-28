using Microsoft.EntityFrameworkCore;
using smERP.Domain.Entities.Product;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class ProductRepository(ProductDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<Product> GetByIdWithProductInstances(int productId)
    {
        return await context.Products.Include(x => x.ProductInstances).FirstOrDefaultAsync(x => x.Id == productId);
    }
}

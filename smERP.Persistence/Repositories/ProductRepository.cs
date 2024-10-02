using Microsoft.EntityFrameworkCore;
using smERP.Domain.Entities;
using smERP.Domain.Entities.Product;
using smERP.Application.Contracts.Persistence;
using smERP.Persistence.Data;
using System.Linq;

namespace smERP.Persistence.Repositories;

public class ProductRepository(ProductDbContext context) : Repository<Product>(context), IProductRepository
{
    public async Task<Product> GetByIdWithProductInstances(int productId)
    {
        return await context.Products.Include(x => x.ProductInstances).FirstOrDefaultAsync(x => x.Id == productId);
    }

    public async Task<IEnumerable<(int ProductInstanceId,bool IsTracked, bool IsWarranted, int? ShelfLifeInDays)>> GetProductInstancesWithProduct(IEnumerable<int> productinstanceIds)
    {
        return await _context.Set<ProductInstance>()
            .Include(x => x.Product)
            .Where(x => productinstanceIds.Contains(x.Id))
            .Select(x => new ValueTuple<int,bool, bool, int?>(
                x.Id,
                x.Product.AreItemsTracked,
                x.Product.IsWarranted,
                x.Product.ShelfLifeInDays
            ))
            .ToListAsync();
    }
}

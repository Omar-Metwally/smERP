using smERP.Domain.Entities.Product;

namespace smERP.Persistence.Contracts;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetByIdWithProductInstances(int productId);
}

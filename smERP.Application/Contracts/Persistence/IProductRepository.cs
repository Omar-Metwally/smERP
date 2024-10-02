using smERP.Domain.Entities.Product;

namespace smERP.Application.Contracts.Persistence;

public interface IProductRepository : IRepository<Product>
{
    Task<Product> GetByIdWithProductInstances(int productId);
    Task<IEnumerable<(int ProductInstanceId, bool IsTracked, bool IsWarranted, int? ShelfLifeInDays)>> GetProductInstancesWithProduct(IEnumerable<int> productinstanceIds);
}

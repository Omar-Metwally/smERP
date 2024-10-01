using smERP.Domain.Entities.Organization;

namespace smERP.Persistence.Contracts;

public interface IBranchRepository : IRepository<Branch>
{
    Task<Branch> GetByIdWithStorageLocations(int Id);
}
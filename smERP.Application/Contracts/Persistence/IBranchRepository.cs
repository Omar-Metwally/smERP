using smERP.Domain.Entities.Organization;

namespace smERP.Application.Contracts.Persistence;

public interface IBranchRepository : IRepository<Branch>
{
    Task<Branch> GetByIdWithStorageLocations(int Id);
}
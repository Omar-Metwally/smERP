using Microsoft.EntityFrameworkCore;
using smERP.Domain.Entities.Organization;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class BranchRepository(ProductDbContext context) : Repository<Branch>(context), IBranchRepository
{
    public async Task<Branch> GetByIdWithStorageLocations(int Id)
    {
        return await context.Branches.Include(x => x.StorageLocations).FirstOrDefaultAsync(x => x.Id == Id);
    }
}
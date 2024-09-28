using smERP.Domain.Entities.Organization;
using smERP.Persistence.Contracts;
using smERP.Persistence.Data;

namespace smERP.Persistence.Repositories;

public class CompanyRepository(AppDbContext context) : Repository<Company>(context), ICompanyRepository
{
}

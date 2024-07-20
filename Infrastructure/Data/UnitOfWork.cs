using smERP.Application.Interfaces;
using smERP.Application.Interfaces.Repositories;
using smERP.Domain.Entities;
using smERP.Infrastructure.Repositories;

namespace smERP.Infrastructure.Data;

public class UnitOfWork<T>(DBContext context) : IUnitOfWork<T> where T : BaseEntity
{
    private readonly DBContext _context = context;
    public IRepository<T> Repository { get; } = new Repository<T>(context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
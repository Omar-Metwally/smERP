using smERP.Application.Interfaces.Repositories;
using smERP.Domain.Entities;

namespace smERP.Application.Interfaces;

public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
{
    public IRepository<T> Repository { get; }
    Task<int> SaveChangesAsync();
}

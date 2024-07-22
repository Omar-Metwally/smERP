using smERP.Application.DTOs.Rquests.Image;
using smERP.Application.Interfaces.Repositories;
using smERP.Domain.Entities;
using smERP.Infrastructure.Repositories;

namespace smERP.Application.Interfaces;

public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
{
    public IRepository<T> Repository { get; }
    public IImageRepository<ImagePersistenceData> Images { get; }
    Task<int> SaveChangesAsync();
}

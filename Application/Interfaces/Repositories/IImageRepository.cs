using smERP.Application.DTOs.Rquests.Image;

namespace smERP.Application.Interfaces.Repositories;

public interface IImageRepository<T> where T : ImagePersistenceData
{
    Task<bool> SaveImage(T image);
}

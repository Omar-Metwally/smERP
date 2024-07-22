using Microsoft.EntityFrameworkCore;
using smERP.Application.DTOs.Rquests.Image;
using smERP.Application.Interfaces.Repositories;

namespace smERP.Infrastructure.Repositories;

public class ImageRepository : IImageRepository<ImagePersistenceData>
{
    public ImageRepository(DbContext context) : base() { }

    public Task<bool> SaveImage(ImagePersistenceData image)
    {
        throw new NotImplementedException();
    }
}

using smERP.Application.DTOs.Image;
using smERP.Application.DTOs.Results.Image;

namespace smERP.Application.Interfaces.Services;

public interface IImageService
{
    Task<ImageURLs?> ProcessImage(ImageProcessingInput image);
}

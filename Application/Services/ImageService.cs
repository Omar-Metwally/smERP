using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using smERP.Application.DTOs.Results;
using smERP.Application.Interfaces.Services;
using smERP.Application.DTOs.Image;
using smERP.Application.DTOs.Results.Image;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Text.RegularExpressions;

namespace smERP.Application.Services;

public class ImageServiceConfig
{
    public int FullScreenWidth { get; set; } = 900;
    public int ThumbnailWidth { get; set; } = 300;
    public int JpegQuality { get; set; } = 90;
}

public class ImageService : IImageService
{
    private readonly IStringLocalizer<ImageService> _localizer;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _wwwRootPath;
    private readonly ImageServiceConfig _config;
    private readonly ILogger<ImageService> _logger;

    public ImageService(
        IWebHostEnvironment webHostEnvironment,
        IStringLocalizer<ImageService> localizer,
        IOptions<ImageServiceConfig> config,
        ILogger<ImageService> logger)
    {
        _localizer = localizer;
        _webHostEnvironment = webHostEnvironment;
        _wwwRootPath = _webHostEnvironment.ContentRootPath;
        _config = config.Value;
        _logger = logger;
    }

    public async Task<ImageURLs?> ProcessImage(ImageProcessingInput image, CancellationToken cancellationToken = default)
    {
        if (image == null || image.Content == null || image.Content.Length == 0)
        {
            return null;
        }

        try
        {
            using var imageResult = await Image.LoadAsync(image.Content, cancellationToken);

            var fullScreenImage = await ProcessImage(imageResult, _config.FullScreenWidth, cancellationToken);
            var thumbnailImage = await ProcessImage(imageResult, _config.ThumbnailWidth, cancellationToken);

            var fullScreenImageURL = await SaveImage(fullScreenImage, image.Path, $"FullScreen_{image.FileName}", cancellationToken);
            var thumbnailImageURL = await SaveImage(thumbnailImage, image.Path, $"Thumbnail_{image.FileName}", cancellationToken);

            return new ImageURLs
            {
                Fullscreen = fullScreenImageURL,
                Thumbnail = thumbnailImageURL
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing image {FileName}", image.FileName);
            return null;
        }
    }

    private static async Task<MemoryStream> ProcessImage(Image image, int resizeWidth, CancellationToken cancellationToken)
    {
        var width = image.Width;
        var height = image.Height;

        if (width > resizeWidth)
        {
            height = (int)((double)resizeWidth / width * height);
            width = resizeWidth;
        }

        var clone = image.Clone(x => x.Resize(width, height));

        var memoryStream = new MemoryStream();
        await clone.SaveAsJpegAsync(memoryStream, new JpegEncoder
        {
            Quality = 90,
            SkipMetadata = true
        }, cancellationToken);

        memoryStream.Position = 0;
        return memoryStream;
    }

    private async Task<string> SaveImage(MemoryStream imageStream, string path, string name, CancellationToken cancellationToken)
    {
        var sanitizedName = SanitizeFileName(name);
        var uploadPath = Path.Combine(_wwwRootPath, path);
        Directory.CreateDirectory(uploadPath);

        var fileName = $"{sanitizedName}.jpg";
        var filePath = Path.Combine(uploadPath, fileName);

        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream, cancellationToken);
            }

            return Path.Combine("/", path, fileName).Replace("\\", "/");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving image {FileName}", fileName);
            throw new IOException(_localizer["ErrorSavingImage", fileName], ex);
        }
    }

    private static string SanitizeFileName(string fileName)
    {
        string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        string invalidReplace = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
        return Regex.Replace(fileName, invalidReplace, "_");
    }
}
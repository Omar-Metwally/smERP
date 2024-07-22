using Microsoft.Extensions.Localization;
using smERP.Application.DTOs.Results;
using smERP.Application.Interfaces.Services;
using smERP.Application.Interfaces;
using smERP.Domain.Entities.Organization;
using smERP.Application.DTOs.Image;
using smERP.Application.DTOs.Results.Image;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace smERP.Application.Services;

public class ImageService : BaseResponseHandler, IImageService
{
    private readonly IUnitOfWork<Company> _unitOfWork;
    private readonly IStringLocalizer<ImageService> _localizer;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _wwwRootPath;
    private const int FullScreenWidth = 900;
    private const int ThumbnailWidth = 300;
    //private readonly IValidator<CreateCompanyRequest> _validator;

    public ImageService(IWebHostEnvironment webHostEnvironment, IUnitOfWork<Company> unitOfWork, IStringLocalizer<ImageService> localizer, IStringLocalizer<BaseResponseHandler> baseLocalizer)
        : base(baseLocalizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _webHostEnvironment = webHostEnvironment;
        _wwwRootPath = _webHostEnvironment.WebRootPath;
    }

    public async Task<ImageURLs?> ProcessImage(ImageProcessingInput image)
    {
        var tasks = new List<Task>();

        var imageResult = await Image.LoadAsync(image.Content);

        MemoryStream processedFullScreenImage;
        MemoryStream processedThumbnailImage;

        string thumbnailImageURL = "";
        string fullScreenImageURL = "";

        try
        {
            tasks.Add(Task.Run(async () =>
            {
                //var storagePath = Path.Combine(Directory.GetCurrentDirectory(), $"{image.Path}");


                //if (!Directory.Exists(storagePath))
                //{
                //    Directory.CreateDirectory(storagePath);
                //}

                processedFullScreenImage = await SavingImage(imageResult, $"FullScreen_{image.FileName}", image.Path, FullScreenWidth);
                processedThumbnailImage = await SavingImage(imageResult, $"Thumbnail_{image.FileName}", image.Path, ThumbnailWidth);

                fullScreenImageURL = await UploadFiles(processedFullScreenImage, image.Path, $"FullScreen_{image.FileName}");
                thumbnailImageURL = await UploadFiles(processedThumbnailImage, image.Path, $"Thumbnail_{image.FileName}");
            }));

            await Task.WhenAll(tasks);

            return new ImageURLs()
            {
                Fullscreen = fullScreenImageURL,
                Thumbnail = thumbnailImageURL
            };
        }
        catch
        {
            return null;
        }


    }

    private static async Task<MemoryStream> SavingImage(Image image, string name, string path, int resizeWidth)
    {

        var width = image.Width;
        var height = image.Height;

        if (width > resizeWidth)
        {
            height = (int)((double)resizeWidth / width * height);
            width = resizeWidth;
        }
        image.Mutate(x => x.Resize(width, height));

        var memoryStream = new MemoryStream();

        //if (File.Exists($"{path}/{name}.jpg"))
        //{
        //    File.Delete($"{path}/{name}.jpg");
        //}

        //await image.SaveAsJpegAsync($"{path}/{name}.jpg", new JpegEncoder
        //{
        //    Quality = 90,
        //    SkipMetadata = true
        //});

        await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
        {
            Quality = 90,
            SkipMetadata = true
        });

        return memoryStream;
        //return await UploadFiles(memoryStream, path, name);


    }

    public async Task<string> UploadFiles(MemoryStream stream, string path, string name)
    {
        stream.Position = 0;
        var uploadPath = Path.Combine(_wwwRootPath, path);
        Directory.CreateDirectory(uploadPath);

        var fileName = $"{name}.jpg";
        var filePath = Path.Combine(uploadPath, fileName);

        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }

            // Return a relative URL path that can be used in your application
            return Path.Combine("/", path, fileName).Replace("\\", "/");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
            return "";
        }
    }

    //public async Task<string> UploadFiles(MemoryStream stream, string path, string name)
    //{
    //    stream.Position = 0;
    //    //using var stream = new MemoryStream();
    //    //await file.CopyToAsync(stream);
    //    var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBd16d14vL8hTQR3ufgrCkvyfghfghfgeBwjnN3koY"));
    //    var a = await auth.SignInWithEmailAndPasswordAsync("gdfgdfg.1fdgfghfdfhdget@gmfgafdfgdfghgdfgdfghidfgl.com", "12345fghfgx6zZsdf!");
    //    var cancellation = new CancellationTokenSource();

    //    var task = new FirebaseStorage(
    //        "fir-e4e25.appspot.com",
    //        new FirebaseStorageOptions
    //        {
    //            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
    //            ThrowOnCancel = true
    //        })
    //        .Child(path)
    //        .Child(name + ".jpg")
    //        .PutAsync(stream, cancellation.Token);

    //    task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
    //    try
    //    {
    //        var downloadUrl = await task;
    //        Console.WriteLine($"Download link:\n{downloadUrl}");
    //        return downloadUrl;
    //    }
    //    catch (Exception ex)
    //    {
    //        return "";
    //    }
    //}
}

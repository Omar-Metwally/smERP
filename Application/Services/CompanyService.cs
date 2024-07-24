using FluentValidation;
using Microsoft.Extensions.Localization;
using smERP.Application.DTOs.Image;
using smERP.Application.DTOs.Results;
using smERP.Application.DTOs.Rquests.Company;
using smERP.Application.Helpers;
using smERP.Application.Interfaces;
using smERP.Application.Interfaces.Services;
using smERP.Domain.Entities.Organization;
using smERP.Shared.Extensions;

namespace smERP.Application.Services;

public class CompanyService : BaseResponseHandler, ICompanyService
{
    private readonly IUnitOfWork<Company> _unitOfWork;
    private readonly IImageService _imageService;
    private readonly IStringLocalizer<CompanyService> _localizer;
    private readonly IValidator<CreateCompanyRequest> _validator;

    public CompanyService(IImageService imageService, IUnitOfWork<Company> unitOfWork, IStringLocalizer<CompanyService> localizer, IStringLocalizer<BaseResponseHandler> baseLocalizer, IValidator<CreateCompanyRequest> validator)
        : base(baseLocalizer)
    {
        _imageService = imageService;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _validator = validator;
    }


    public async Task<BaseResponse<int>> CreateCompany(CreateCompanyRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return BadRequest<int>(_localizer["Failure"], errors);
        }
            
        var base64Handler = new Base64ImageHandler();

        var coverImageMemoryStream = base64Handler.ConvertBase64ToMemoryStream(base64Handler.TryPrepareBase64Image(request.CoverImage));

        var logoImageMemoryStream = base64Handler.ConvertBase64ToMemoryStream(base64Handler.TryPrepareBase64Image(request.LogoImage));


        var coverImage = await _imageService.ProcessImage(new ImageProcessingInput
        {
            FileName = request.Name + "_coverImage",
            Content = coverImageMemoryStream,
            Type = "",
            Path = "Images/Meal"
        });

        var logoImage = await _imageService.ProcessImage(new ImageProcessingInput
        {
            FileName = request.Name + "_logoImage",
            Content = logoImageMemoryStream,
            Type = "",
            Path = "Images/Meal"
        });

        var companyCreated = await _unitOfWork.Repository.Add(MappingExtensions.ToCompany(request.Name, coverImage, logoImage));

        await _unitOfWork.SaveChangesAsync();
        return Success(companyCreated.Id, _localizer["Success"]);
    }
}

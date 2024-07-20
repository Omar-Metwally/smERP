using FluentValidation;
using Microsoft.Extensions.Localization;
using smERP.Application.DTOs.Results;
using smERP.Application.DTOs.Rquests.Company;
using smERP.Application.Interfaces;
using smERP.Application.Interfaces.Services;
using smERP.Domain.Entities.Organization;
using smERP.Shared.Extensions;

namespace smERP.Application.Services;

public class CompanyService : BaseResponseHandler, ICompanyService
{
    private readonly IUnitOfWork<Company> _unitOfWork;
    private readonly IStringLocalizer<CompanyService> _localizer;
    private readonly IValidator<CreateCompanyRequest> _validator;

    public CompanyService(IUnitOfWork<Company> unitOfWork, IStringLocalizer<CompanyService> localizer, IStringLocalizer<BaseResponseHandler> baseLocalizer, IValidator<CreateCompanyRequest> validator)
        : base(baseLocalizer)
    {
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
            return BadRequest<int>(_localizer["Success"], errors);
        }

        var companyCreated = await _unitOfWork.Repository.Add(MappingExtensions.ToCompany(request));

        await _unitOfWork.SaveChangesAsync();
        return Success(companyCreated.Id, _localizer["Success"]);
    }
}

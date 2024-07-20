using FluentValidation;
using Microsoft.Extensions.Localization;
using smERP.Application.DTOs.Rquests.Company;
using smERP.Application.Interfaces;
using smERP.Domain.Entities.Organization;

namespace smERP.Application.Validators;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    private readonly IUnitOfWork<Company> _unitOfWork;

    public CreateCompanyRequestValidator(IUnitOfWork<Company> unitOfWork, IStringLocalizer<CreateCompanyRequestValidator> localizer)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x)
            .MustAsync(IsCompanyTableEmpty)
            .WithMessage("Only one company can be created")
            .When(x => true, ApplyConditionTo.CurrentValidator);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required")
            .MaximumLength(100).WithMessage("Company name must not exceed 100 characters")
            .MustAsync(BeUniqueName).WithMessage("Company name must be unique")
            .When(x => true, ApplyConditionTo.CurrentValidator);

    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.Repository.DoesExist(c => c.Name == name);
    }

    private async Task<bool> IsCompanyTableEmpty(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository.IsTableEmpty();
    }
}
using FluentValidation;
using smERP.Application.Features.Attributes.Commands.Models;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;

namespace smERP.Application.Features.Attributes.Commands.Validators;

public class AddAttributeCommandValidator : AbstractValidator<AddAttributeCommandModel>
{
    public AddAttributeCommandValidator()
    {
        RuleFor(c => c.ArabicName).NotEmpty().WithMessage(c =>
        {
            var fieldName = SharedResourcesKeys.NameAr.Localize();
            var errorMessage = SharedResourcesKeys.Required_FieldName.Localize(fieldName);
            return errorMessage;
        });

        RuleFor(c => c.EnglishName).NotEmpty().WithMessage(c =>
        {
            var fieldName = SharedResourcesKeys.NameEn.Localize();
            var errorMessage = SharedResourcesKeys.Required_FieldName.Localize(fieldName);
            return errorMessage;
        });
    }
}
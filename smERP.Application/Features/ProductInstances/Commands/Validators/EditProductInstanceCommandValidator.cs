using FluentValidation;
using smERP.Application.Features.ProductInstances.Commands.Models;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;

namespace smERP.Application.Features.ProductInstances.Commands.Validators;

public class EditProductInstanceCommandValidator : AbstractValidator<EditProductInstanceCommandModel>
{
    public EditProductInstanceCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .GreaterThan(0)
            .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Product.Localize()));

        RuleFor(command => command.ProductInstanceId)
            .GreaterThan(0)
            .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Product.Localize()));

        //RuleFor(command => command.BuyingPrice)
        //    .Must(MustBePositiveIfNotNull)
        //    .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.BuyingPrice.Localize()));

        RuleFor(command => command.SellingPrice)
            .Must(MustBePositiveIfNotNull)
            .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.SellingPrice.Localize()));

        //RuleFor(command => command.QuantityInStock)
        //    .Must(MustBePositiveIfNotNull)
        //    .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.Quantity.Localize()));

        RuleFor(command => command.ProductInstanceAttributeValues)
            .NotEmpty()
            .WithMessage(SharedResourcesKeys.___ListMustContainAtleastOneItem.Localize(SharedResourcesKeys.AttributeList.Localize()))
            .Must(MustHaveUniqueAttributeIdsIfNotNull)
            .WithMessage(SharedResourcesKeys.___ListCannotContainDuplicates.Localize(SharedResourcesKeys.AttributeList.Localize()));
    }

    private bool MustHaveUniqueAttributeIdsIfNotNull(List<ProductInstanceAttributeValue>? attributeValues)
    {
        if (attributeValues == null)
            return true;

        return attributeValues.DistinctBy(x => x.AttributeId).Count() == attributeValues.Count;
    }

    private bool MustBePositiveIfNotNull(decimal? number)
    {
        if (number == null)
            return true;

        if (number > 0)
            return true;

        return false;
    }

    private bool MustBePositiveIfNotNull(int? number)
    {
        if (number == null)
            return true;

        if (number > 0)
            return true;

        return false;
    }
}
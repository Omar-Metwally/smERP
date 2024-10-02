using FluentValidation;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;

namespace smERP.Application.Features.ProcurementTransactions.Commands.Validators;

public class AddProcurementTransactionCommandValidator : AbstractValidator<AddProcurementTransactionCommandModel>
{
    public AddProcurementTransactionCommandValidator()
    {
        RuleFor(command => command.StorageLocationId)
            .GreaterThan(0)
            .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.StorageLocation.Localize()));

        RuleFor(command => command.SupplierId)
            .GreaterThan(0)
            .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Supplier.Localize()));

        RuleFor(command => command.BranchId)
            .GreaterThan(0)
            .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Branch.Localize()));

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductInstanceId)
                .GreaterThan(0)
                .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Product.Localize()));

            product.RuleFor(p => p.Quantity)
                .GreaterThan(0)
                .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.Quantity.Localize()));
        });

        RuleFor(command => command.Payment)
            .Must(payment => payment == null || payment.PayedAmount > 0)
            .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.PayedAmount.Localize()))
            .When(command => command.Payment != null);

    }
}

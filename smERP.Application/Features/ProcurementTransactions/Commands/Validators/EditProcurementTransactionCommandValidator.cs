using FluentValidation;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;

namespace smERP.Application.Features.ProcurementTransactions.Commands.Validators;

public class EditProcurementTransactionCommandValidator : AbstractValidator<EditProcurementTransactionCommandModel>
{
    public EditProcurementTransactionCommandValidator()
    {
        RuleFor(command => command.SupplierId)
            .GreaterThan(0)
            .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Supplier.Localize()));

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.ProductInstanceId)
                .GreaterThan(0)
                .WithMessage(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Product.Localize()));

            product.RuleFor(p => p.Quantity)
                .GreaterThan(0)
                .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.Quantity.Localize()));
        });

        RuleForEach(command => command.Payments)
            .Must(payment => payment == null || payment.PayedAmount > 0)
            .WithMessage(SharedResourcesKeys.___MustBeAPositiveNumber.Localize(SharedResourcesKeys.PayedAmount.Localize()));

    }
}

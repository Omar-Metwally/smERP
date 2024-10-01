using FluentValidation;
using smERP.Application.Features.ProcurementTransactions.Commands.Models;

namespace smERP.Application.Features.ProcurementTransactions.Commands.Validators;

public class EditProcurementTransactionCommandValidator : AbstractValidator<EditProcurementTransactionCommandModel>
{
    public EditProcurementTransactionCommandValidator()
    {

    }
}

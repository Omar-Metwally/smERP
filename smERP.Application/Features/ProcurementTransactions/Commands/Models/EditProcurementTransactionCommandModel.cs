using MediatR;
using smERP.SharedKernel.Responses;

namespace smERP.Application.Features.ProcurementTransactions.Commands.Models;

public record EditProcurementTransactionCommandModel(int ProcurementTransactionId, int? SupplierId, List<ProductEntry>? Products, List<UpdatePayment>? Payments) : IRequest<IResultBase>;

public record UpdatePayment (int PaymentTransactionId, decimal PayedAmount, string PaymentMethod);
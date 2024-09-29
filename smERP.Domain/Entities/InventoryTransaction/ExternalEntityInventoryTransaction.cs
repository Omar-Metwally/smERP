
using smERP.Domain.ValueObjects;
using smERP.SharedKernel.Responses;
using System.Collections.Generic;
using System.Net;

namespace smERP.Domain.Entities.InventoryTransaction;

public abstract class ExternalEntityInventoryTransaction : InventoryTransaction
{
    public ICollection<TransactionPayment> Payments { get; private set; } = null!;

    protected ExternalEntityInventoryTransaction(DateTime transactionDate, ICollection<TransactionPayment>? payments, ICollection<InventoryTransactionItem> items) : base(transactionDate, items)
    {
        Payments = payments ?? new List<TransactionPayment>();
    }

    protected ExternalEntityInventoryTransaction() { }

    protected static IResult<(List<TransactionPayment>, List<InventoryTransactionItem>)> Create(List<(decimal PayedAmount, string PaymentMethod)>? payments,List<(int ProductInstanceId, int Quantity, decimal? UnitPrice)> transactionItems)
    {
        var baseDetailsCreateResult = CreateBaseDetails(transactionItems);
        if (baseDetailsCreateResult.IsFailed)
            return baseDetailsCreateResult.ChangeType((new List<TransactionPayment>(), new List<InventoryTransactionItem>()));

        var transactionPayment = new List<TransactionPayment>();

        if (payments != null && payments.Count > 0)
        {
            foreach (var (payedAmount, paymentMethod) in payments)
            {
                var transactionPaymentCreateResult = TransactionPayment.Create(payedAmount, paymentMethod);
                if (transactionPaymentCreateResult.IsFailed)
                    return transactionPaymentCreateResult.ChangeType((new List<TransactionPayment>(), new List<InventoryTransactionItem>()));

                transactionPayment.Add(transactionPaymentCreateResult.Value);
            }
        }

        return new Result<(List<TransactionPayment>, List<InventoryTransactionItem>)>((transactionPayment, baseDetailsCreateResult.Value));
    }

}

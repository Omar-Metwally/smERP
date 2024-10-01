
using smERP.Domain.Entities.ExternalEntities;
using smERP.SharedKernel.Localizations.Extensions;
using smERP.SharedKernel.Localizations.Resources;
using smERP.SharedKernel.Responses;
using System.Net;

namespace smERP.Domain.Entities.InventoryTransaction;

public class ProcurementTransaction : ExternalEntityInventoryTransaction
{
    public int SupplierId { get; private set; }
    public virtual Supplier Supplier { get; private set; } = null!;
    public ProcurementTransaction(int storageLocationId, int supplierId, DateTime transactionDate,ICollection<TransactionPayment>? payments, ICollection<InventoryTransactionItem> items) : base(storageLocationId, transactionDate, payments, items)
    {
        SupplierId = supplierId;
    }
    private ProcurementTransaction() { }

    public static IResult<ProcurementTransaction> Create(int storageLocationId, int supplierId, List<(decimal PayedAmount, string PaymentMethod)>? payments, List<(int ProductInstanceId, int Quantity, decimal UnitPrice)> transactionItems, DateTime? transactionDate = null)
    {
        var baseDetailsCreateResult = Create(payments, transactionItems);
        if (baseDetailsCreateResult.IsFailed)
            return baseDetailsCreateResult.ChangeType(new ProcurementTransaction());

        if (supplierId < 0)
            return new Result<ProcurementTransaction>()
                .WithError(SharedResourcesKeys.Required_FieldName.Localize(SharedResourcesKeys.Supplier.Localize()))
                .WithStatusCode(HttpStatusCode.BadRequest);

        return new Result<ProcurementTransaction>(new ProcurementTransaction(storageLocationId, supplierId, transactionDate??DateTime.UtcNow, baseDetailsCreateResult.Value.Item1, baseDetailsCreateResult.Value.Item2));
    }

    public override string GetTransactionType() => "Procurement";
}

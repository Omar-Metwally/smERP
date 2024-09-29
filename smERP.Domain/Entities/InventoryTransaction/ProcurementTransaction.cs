
using smERP.Domain.Entities.ExternalEntities;

namespace smERP.Domain.Entities.InventoryTransaction;

public class ProcurementTransaction : ExternalEntityInventoryTransaction
{
    public int SupplierId { get => ExternalEntityId; private set => ExternalEntityId = value; }
    public virtual Supplier Supplier { get; private set; } = null!;
    public ProcurementTransaction(DateTime transactionDate, ICollection<InventoryTransactionItem> items) : base(transactionDate, items)
    {
    }
    private ProcurementTransaction() { }

    public override string GetTransactionType() => "Procurement";
}

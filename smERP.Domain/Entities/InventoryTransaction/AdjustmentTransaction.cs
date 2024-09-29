
namespace smERP.Domain.Entities.InventoryTransaction;

public class AdjustmentTransaction : InventoryTransaction
{
    public AdjustmentTransaction(DateTime transactionDate, ICollection<InventoryTransactionItem> items) : base(transactionDate, items)
    {
    }

    private AdjustmentTransaction() { }

    public override string GetTransactionType() => "Adjustment";
}

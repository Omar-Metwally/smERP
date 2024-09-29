using System.ComponentModel.DataAnnotations.Schema;

namespace smERP.Domain.Entities.InventoryTransaction;

public abstract class ExternalEntityInventoryTransaction : InventoryTransaction
{
    [NotMapped]
    public int ExternalEntityId { get; protected set; }
    public ICollection<TransactionPayment> Payments { get; private set; } = new List<TransactionPayment>();

    protected ExternalEntityInventoryTransaction(DateTime transactionDate, ICollection<InventoryTransactionItem> items) : base(transactionDate, items)
    {
    }

    protected ExternalEntityInventoryTransaction() { }
}

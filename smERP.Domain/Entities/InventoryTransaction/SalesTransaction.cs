
namespace smERP.Domain.Entities.InventoryTransaction;

public class SalesTransaction : ExternalEntityInventoryTransaction
{
    public int ClientId { get => ExternalEntityId; private set => ExternalEntityId = value; }
    public SalesTransaction(DateTime transactionDate, ICollection<InventoryTransactionItem> items) : base(transactionDate, items)
    {
    }
    
    private SalesTransaction() { }

    public override string GetTransactionType() => "Sales";
}

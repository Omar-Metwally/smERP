
namespace smERP.Domain.Entities.InventoryTransaction;

public class SalesTransaction : ExternalEntityInventoryTransaction
{
    public int ClientId { get; private set; }
    public SalesTransaction(DateTime transactionDate, ICollection<TransactionPayment> payments, ICollection<InventoryTransactionItem> items) : base(transactionDate, payments, items)
    {
    }
    
    private SalesTransaction() { }

    public override string GetTransactionType() => "Sales";
}

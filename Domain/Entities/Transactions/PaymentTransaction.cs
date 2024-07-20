using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class PaymentTransaction : BaseTransaction
{
    public int? ReferencingTransactionId { get; set; }
    public string? SupplierId { get; set; }
    public string? CustomerId { get; set; }
    public virtual BaseTransaction ReferencingTransaction { get; set; }
    public virtual Supplier? Supplier { get; set; }
    public virtual Customer? Customer { get; set; }
}
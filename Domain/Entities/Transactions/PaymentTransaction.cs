using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class PaymentTransaction : BaseTransaction
{
    public int? ReferencingTransactionID { get; set; }
    public string? SupplierID { get; set; }
    public string? CustomerID { get; set; }
    public virtual BaseTransaction ReferencingTransaction { get; set; }
    public virtual Supplier? Supplier { get; set; }
    public virtual Customer? Customer { get; set; }
}
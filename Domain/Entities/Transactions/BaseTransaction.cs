using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.User;
using smERP.Enums;

namespace smERP.Domain.Entities.Transactions;

public class BaseTransaction : BaseEntity
{
    public string EmployeeId { get; set; }
    public int BranchId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsCanceled { get; set; }
    public int? TotalAmountInCents { get; set; }
    public int? DiscountInCents { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual Employee Employee { get; set; }
    public ICollection<PaymentTransaction> TransactionPayments { get; set; }
    public ICollection<TransactionChange> TransactionChanges { get; set; }
    public ICollection<TransactionItem> TransactionItems { get; set; }
}

using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class BuyTransaction : BaseTransaction
{
    public string SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual BaseTransaction BaseTransaction { get; set; }
}

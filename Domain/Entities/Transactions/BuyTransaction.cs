using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class BuyTransaction : BaseTransaction
{
    public string SupplierId { get; set; }
    public int StorageLocationId { get; set; }
    public virtual StorageLocation StorageLocation { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual BaseTransaction BaseTransaction { get; set; }
}
 
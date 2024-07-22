using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class SellTransaction : BaseTransaction
{
    public string CustomerId { get; set; }
    public int StorageLocationId { get; set; }
    public virtual StorageLocation StorageLocation { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual BaseTransaction BaseTransaction { get; set; }
}

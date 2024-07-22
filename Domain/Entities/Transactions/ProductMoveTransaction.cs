using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class ProductMoveTransaction : BaseTransaction
{
    public int FromStorageLocationId { get; set; }
    public int ToStorageLocationId { get; set; }
    public int Quantity { get; set; }
    public string FromEmployeeId { get; set; }
    public string ToEmployeeId { get; set; }
    public virtual StorageLocation FromStorageLocation { get; set; }
    public virtual StorageLocation ToStorageLocation { get; set; }
    public virtual Employee FromEmployee { get; set; }
    public virtual Employee ToEmployee { get; set; }
}

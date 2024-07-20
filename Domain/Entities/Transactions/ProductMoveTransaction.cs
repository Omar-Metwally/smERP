using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.User;
using System.Security.Cryptography.X509Certificates;

namespace smERP.Domain.Entities.Transactions;

public class ProductMoveTransaction : BaseTransaction
{
    public int FromStorageLocationID { get; set; }
    public int ToStorageLocationID { get; set; }
    public int Quantity { get; set; }
    public string FromEmployeeID { get; set; }
    public string ToEmployeeID { get; set; }
    public virtual StorageLocation FromStorageLocation { get; set; }
    public virtual StorageLocation ToStorageLocation { get; set; }
    public virtual Employee FromEmployee { get; set; }
    public virtual Employee ToEmployee { get; set; }
}

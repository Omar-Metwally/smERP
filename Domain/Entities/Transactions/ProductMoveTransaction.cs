using smERP.Domain.Entities.Organization;
using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Transactions;

public class ProductMoveTransaction : BaseTransaction
{
    public int FromBranchID { get; set; }
    public int ToBranchID { get; set; }
    public int Quantity { get; set; }
    public string FromEmployeeID { get; set; }
    public string ToEmployeeID { get; set; }
    public virtual Branch FromBranch { get; set; }
    public virtual Branch ToBranch { get; set; }
    public virtual Employee FromEmployee { get; set; }
    public virtual Employee ToEmployee { get; set; }
}

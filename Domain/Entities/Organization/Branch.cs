using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Organization;

public class Branch : BaseEntity
{
    public string Name { get; set; }
    public string BranchManagerID { get; set; }
    public int CompanyID { get; set; }
    public virtual Company Company { get; set; }
    public virtual Employee BranchManager { get; set; }
    public ICollection<Employee> BranchEmployees { get; set; }
}

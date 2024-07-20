using smERP.Domain.Entities.User;

namespace smERP.Domain.Entities.Organization;

public class Branch : BaseEntity
{
    public string Name { get; set; }
    public string BranchManagerId { get; set; }
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public virtual Employee BranchManager { get; set; }
    public ICollection<Employee> BranchEmployees { get; set; }
}

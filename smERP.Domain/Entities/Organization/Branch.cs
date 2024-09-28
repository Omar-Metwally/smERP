
using smERP.Domain.ValueObjects;

namespace smERP.Domain.Entities.Organization;

public class Branch : Entity, IAggregateRoot
{
    public BilingualName Name { get; private set; }
    public string BranchManagerId { get; private set; }
    public int CompanyId { get; private set; }
    //public virtual Employee BranchManger { get; private set; }
    //public ICollection<Employee> BranchEmployees { get; private set; }

    public Branch(BilingualName name, int companyId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CompanyId = companyId;
        //BranchEmployees = new List<Employee>();
    }

    public void UpdateName(BilingualName name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public void SetBranchManager(string branchManagerId)
    {
        if (string.IsNullOrWhiteSpace(branchManagerId))
            throw new ArgumentException("Branch manager ID cannot be empty.", nameof(branchManagerId));

        BranchManagerId = branchManagerId;
    }

    //public void AddEmployee(Employee employee)
    //{
    //    if (employee == null)
    //        throw new ArgumentNullException(nameof(employee));

    //    BranchEmployees.Add(employee);
    //}
}

using smERP.Domain.Entities.Organization;

namespace smERP.Domain.Entities.User;

public class Employee : BaseUser
{
    public string? ManagerId { get; set; }
    public int DepartmentId { get; set; }
    public int BranchId { get; set; }
    public int SalaryInCents { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual Department Department { get; set; }
    public virtual Employee? EmployeeManager { get; set; }
    public ICollection<Employee>? ManagedEmployees { get; set; }
}
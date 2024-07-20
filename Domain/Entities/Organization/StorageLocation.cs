namespace smERP.Domain.Entities.Organization;

public class StorageLocation
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public string Name { get; set; }
    public virtual Branch Branch { get; set; }
}

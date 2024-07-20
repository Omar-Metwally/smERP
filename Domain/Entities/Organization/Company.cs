namespace smERP.Domain.Entities.Organization;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string LogoImage { get; set; }
    public string CoverImage { get; set; }
}

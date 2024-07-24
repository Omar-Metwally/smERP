namespace smERP.Domain.Entities.Organization;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string? ThumbnailLogoImage { get; set; }
    public string? FullscreenLogoImage { get; set; }
    public string? ThumbnailCoverImage { get; set; }
    public string? FullscreenCoverImage { get; set; }
}

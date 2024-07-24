namespace smERP.Application.DTOs.Rquests.Company;

public record CreateCompanyRequest
{
    public string Name { get; init; }
    public string? CoverImage { get; init; }
    public string? LogoImage { get; init; }
}

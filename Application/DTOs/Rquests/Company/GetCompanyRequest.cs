namespace smERP.Application.DTOs.Rquests.Company;

public record GetCompanyRequest
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string LogoImagePath { get; init; }
    public string CoverImagePath { get; init; }
}

using smERP.Application.DTOs.Rquests.Company;
using smERP.Domain.Entities.Organization;

namespace smERP.Shared.Extensions;

public static class MappingExtensions
{
    // Company and BookDto conversions
    public static IQueryable<GetCompanyRequest> ToCompanyGetRequest(this IQueryable<Company> company) 
    {
        return company.Select(x => new GetCompanyRequest()
        {
            ID = x.ID,
            Name = x.Name,
            LogoImagePath = x.LogoImage,
            CoverImagePath = x.CoverImage,
        });
    }
    public static Company ToCompany(CreateCompanyRequest companydto)
    {
        return new Company()
        {
            Name = companydto.Name,
        };
    }
}

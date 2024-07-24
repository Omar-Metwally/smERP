using smERP.Application.DTOs.Results.Image;
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
            ID = x.Id,
            Name = x.Name,
            LogoImagePath = x.FullscreenLogoImage?? "",
            CoverImagePath = x.FullscreenCoverImage?? "",
        });
    }
    public static Company ToCompany(string name, ImageURLs coverImage, ImageURLs logoImage)
    {
        return new Company()
        {
            Name = name,
            FullscreenCoverImage = coverImage.Fullscreen,
            ThumbnailCoverImage = coverImage.Thumbnail,
            FullscreenLogoImage = logoImage.Fullscreen,
            ThumbnailLogoImage = logoImage.Thumbnail
        };
    }
}

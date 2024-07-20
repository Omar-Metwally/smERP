namespace smERP.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

public class BaseUser : IdentityUser
{
    public string? AddrsCountry { get; set; }
    public string? AddrsState { get; set; }
    public string? AddrsDistrict { get; set; }
    public string? AddrsStreet { get; set; }
    public int? TotalAmountOwedInCents { get; set; }
}

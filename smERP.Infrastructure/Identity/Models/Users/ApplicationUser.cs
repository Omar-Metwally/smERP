using Microsoft.AspNetCore.Identity;
using smERP.Domain.Entities.User;

namespace smERP.Infrastructure.Identity.Models.Users;

public class ApplicationUser : IdentityUser
{
    public virtual Employee Employee { get; set; } = null!;

    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; } = DateTime.MinValue;

    public void GenerateRefreshToken()
    {
        RefreshToken = Guid.NewGuid().ToString();
        RefreshTokenExpiration = DateTime.UtcNow.AddDays(30);
    }

    public bool IsRefreshTokenValid(string token)
    {
        return RefreshToken == token && RefreshTokenExpiration > DateTime.UtcNow;
    }

    public bool IsExistingRefreshTokenValid()
    {
        return RefreshTokenExpiration > DateTime.UtcNow;
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiration = DateTime.MinValue;
    }
}
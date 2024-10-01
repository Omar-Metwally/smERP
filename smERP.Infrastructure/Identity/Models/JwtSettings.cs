namespace smERP.Infrastructure.Identity.Models;

public record JwtSettings(string Key, string Issuer, string Audience, double DurationInMinutes);

using Serilog.Core;
using Serilog.Events;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace smERP.Middleware;

public class UserEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("UserName", userName ?? "anonymous"));
    }
}

using AuroraRates.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace AuroraRates.Infrastructure.Authentication;

public class CurrentUserDataService : ICurrentUserDataService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserDataService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string? Id => _httpContextAccessor.HttpContext?.User.FindFirst("Id")?.Value;
    public string? Email => _httpContextAccessor.HttpContext?.User.FindFirst("Email")?.Value;
    public string? Username => _httpContextAccessor.HttpContext?.User.FindFirst("Username")?.Value;
}
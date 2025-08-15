namespace AuroraRates.Infrastructure.Authentication;

public class JwtSettings
{
    public string SecretKey { get; set; } = String.Empty;
    public int ExpiresInHours { get; set; }
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
}
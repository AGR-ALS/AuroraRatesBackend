using AuroraRates.Domain.Models;

namespace AuroraRates.Domain.Abstractions.JWT;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
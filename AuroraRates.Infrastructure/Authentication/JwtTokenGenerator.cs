using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuroraRates.Domain.Abstractions.JWT;
using AuroraRates.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuroraRates.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    
    public string GenerateToken(User user)
    {
        Claim[] claims = [new ("Id", user.Id.ToString()), new Claim("Nickname", user.Nickname), new Claim("Email", user.Email)];
        
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            signingCredentials : signingCredentials,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiresInHours));
        
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}
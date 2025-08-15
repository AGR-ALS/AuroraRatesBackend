namespace AuroraRates.Domain.Abstractions.JWT;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool Verify(string password, string hashedPassword);
}
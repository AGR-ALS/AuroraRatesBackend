namespace AuroraRates.Domain.Abstractions.Users;

public interface IUserService
{
    Task Register(string nickname, string password, string email);
    Task<string> Login(string email, string password);
}
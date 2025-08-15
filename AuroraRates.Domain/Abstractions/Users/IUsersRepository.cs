using AuroraRates.Domain.Models;

namespace AuroraRates.Domain.Abstractions.Users;

public interface IUsersRepository
{
    Task AddUserAsync(User user);
    Task<User> FindUserByEmailAsync(string email);
}
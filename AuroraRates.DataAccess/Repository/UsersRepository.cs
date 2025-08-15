using AuroraRates.DataAccess.Entity;
using AuroraRates.Domain.Abstractions.Users;
using AuroraRates.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuroraRates.DataAccess.Repository;

public class UsersRepository : IUsersRepository
{
    private readonly AuroraRatesDatabaseContext _context;

    public UsersRepository(AuroraRatesDatabaseContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        var userEntity = new UserEntity
        {
            Id = user.Id,
            Nickname = user.Nickname,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
        };
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<User> FindUserByEmailAsync(string email)
    {
        var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) ?? throw new KeyNotFoundException();
        
        return new User(userEntity.Id, userEntity.Nickname, userEntity.PasswordHash, userEntity.Email);
    }
}
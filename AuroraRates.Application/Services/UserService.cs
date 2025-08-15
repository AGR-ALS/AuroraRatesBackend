using AuroraRates.Domain.Abstractions.JWT;
using AuroraRates.Domain.Abstractions.Users;
using AuroraRates.Domain.Models;

namespace AuroraRates.Application.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserService(IPasswordHasher passwordHasher, IUsersRepository usersRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task Register(string nickname, string password, string email)
    {
        var hashedPassword = _passwordHasher.HashPassword(password);
        
        var user = new User(Guid.NewGuid(), nickname, hashedPassword, email);
        await _usersRepository.AddUserAsync(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await  _usersRepository.FindUserByEmailAsync(email);
        
        var result = _passwordHasher.Verify(password, user.PasswordHash);
        
        if(result == false)
            throw new ApplicationException("Invalid password");
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        return token;
    }
    
    
}
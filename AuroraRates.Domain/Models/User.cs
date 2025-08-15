namespace AuroraRates.Domain.Models;

public  class User
{
    public User(Guid id, string nickname, string? passwordHash, string email)
    {
        Id = id;
        Nickname = nickname;
        Email = email;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; set; }
    public string Nickname { get; set; }    
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}

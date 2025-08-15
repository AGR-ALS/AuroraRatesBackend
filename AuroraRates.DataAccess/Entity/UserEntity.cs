namespace AuroraRates.DataAccess.Entity;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public List<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
}

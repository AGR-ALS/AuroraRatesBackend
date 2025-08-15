namespace AuroraRates.DataAccess.Entity;

public class ReviewEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid MediaTypeId { get; set; }
    public MediaTypeEntity? MediaType { get; set; }
    public string? ImageUrl {get; set;}

}

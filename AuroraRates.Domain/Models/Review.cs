namespace AuroraRates.Domain.Models;

public class Review
{
    public Review(Guid id, string title, string content, DateTime createdAt, User user, MediaType mediaType) 
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        User = user;
        MediaType = mediaType;
    }
    
    public Review(Guid id, string title, string content, User user, MediaType mediaType) 
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = DateTime.UtcNow;
        User = user;
        MediaType = mediaType;
    }

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public User User { get; set; }
    public MediaType MediaType { get; set; }
    
    public string? ImageUrl {get; set;}

}

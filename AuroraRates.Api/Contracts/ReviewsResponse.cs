namespace AuroraRates.Api.Contracts;

public record ReviewsResponse(Guid Id, string Title, string Content, DateTime CreatedAt, string AuthorNickname, string MediaType, string? ImagePath);
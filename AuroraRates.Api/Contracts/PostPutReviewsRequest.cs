namespace AuroraRates.Api.Contracts;

public record PostPutReviewsRequest(string Title, string Content, string MediaTypeName, IFormFile? Image);
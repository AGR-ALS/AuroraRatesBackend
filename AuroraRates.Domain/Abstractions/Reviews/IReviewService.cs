using AuroraRates.Domain.Models;

namespace AuroraRates.Domain.Abstractions.Reviews;

public interface IReviewService
{
    Task<List<Review>> GetAllReviewsAsync();
    Task<Review> GetReviewByIdAsync(Guid id);
    Task<Guid> CreateReviewAsync(Review review);
    Task<Guid> UpdateReviewAsync(Review review);
    Task<Guid> DeleteReviewAsync(Guid id);
}
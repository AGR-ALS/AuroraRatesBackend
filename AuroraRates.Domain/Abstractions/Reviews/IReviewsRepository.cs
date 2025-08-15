using AuroraRates.Domain.Models;

namespace AuroraRates.Domain.Abstractions.Reviews;

public interface IReviewsRepository
{
    Task<List<Review>> GetReviewsAsync();
    Task<Review> GetReviewByIdAsync(Guid id);
    Task<Guid> CreateReviewAsync(Review review);
    Task<Guid> UpdateReviewAsync(Review review);
    Task<Guid> DeleteReviewAsync(Guid id);
}
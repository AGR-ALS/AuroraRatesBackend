using AuroraRates.Domain.Abstractions.Reviews;
using AuroraRates.Domain.Models;

namespace AuroraRates.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewsRepository _reviewsRepository;

    public ReviewService(IReviewsRepository reviewsRepository)
    {
        _reviewsRepository = reviewsRepository;
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        return await _reviewsRepository.GetReviewsAsync();
    }

    public async Task<Review> GetReviewByIdAsync(Guid id)
    {
        return await _reviewsRepository.GetReviewByIdAsync(id);
    }

    public async Task<Guid> CreateReviewAsync(Review review)
    {
        return await _reviewsRepository.CreateReviewAsync(review);
    }

    public async Task<Guid> UpdateReviewAsync(Review review)
    {
        return await _reviewsRepository.UpdateReviewAsync(review);
    }

    public async Task<Guid> DeleteReviewAsync(Guid id)
    {
        return await _reviewsRepository.DeleteReviewAsync(id);
    }
}